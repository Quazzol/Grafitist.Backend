using AutoMapper;
using Grafitist.Contracts.Cart.Request;
using Grafitist.Contracts.Cart.Response;
using Grafitist.Contracts.Order.Request;
using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Grafitist.Services.Cart.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Misc.Enums;

namespace Grafitist.CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly ICartLineRepository _lineRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly IPriceManager _priceManager;
    private readonly IOrderService _orderService;

    public CartService(ICartRepository repository, ICartLineRepository lineRepository, IUserContext userContext, IMapper mapper, IPriceManager priceManager, IOrderService orderService)
    {
        _repository = repository;
        _lineRepository = lineRepository;
        _userContext = userContext;
        _mapper = mapper;
        _priceManager = priceManager;
        _orderService = orderService;
    }

    public async Task<CartDTO> ApproveCart(Guid id)
    {
        var cart = _mapper.Map<CartDTO>(await _repository.Update(new CartModel { Id = id, Status = CartStatus.Completed }));
        await _orderService.Insert(CreateOrder());
        return cart;

        OrderInsertDTO CreateOrder()
        {
            var order = new OrderInsertDTO
            {
                CartId = cart.Id,
                Status = OrderStatus.Pending,
                UserId = cart.UserId
            };

            var orderLines = new List<OrderLineInsertDTO>();
            foreach (var cartLine in cart.Lines!)
            {
                orderLines.Add(CreateOrderLine(order.Id, cartLine));
            }

            order.Lines = orderLines;
            return order;
        }

        OrderLineInsertDTO CreateOrderLine(Guid orderId, ITransactionLine cartLine)
        {
            return new OrderLineInsertDTO
            {
                OrderId = orderId,
                Quantity = cartLine.ProductId,
                Price = cartLine.Price,
                DiscountedPrice = cartLine.DiscountedPrice,
                CampaignId = cartLine.CampaignId,
                ProductId = cartLine.ProductId
            };
        }
    }

    public async Task ClearCart(Guid id)
    {
        await _repository.Update(new CartModel { Id = id, Status = CartStatus.Canceled });
    }

    public async Task DeleteLine(Guid id)
    {
        await _lineRepository.Delete(id);
    }

    public async Task<CartDTO> Get(Guid? id)
    {
        var cart = _mapper.Map<CartDTO>(await _repository.Get(id));
        await CalculateAmounts(cart);
        return cart;
    }

    public async Task<IEnumerable<CartDTO>> GetByUser(Guid id, Pager pager)
    {
        var carts = _mapper.Map<IEnumerable<CartDTO>>(await _repository.GetByUser(id, pager));
        foreach (var cart in carts)
        {
            await CalculateAmounts(cart);
        }
        return carts;
    }

    public async Task<CartDTO> Insert(CartLineInsertDTO dto)
    {
        CartModel? cartModel;
        if (dto.CartId.IsEmpty())
        {
            cartModel = new CartModel
            {
                Id = Guid.NewGuid(),
                Status = CartStatus.Pending,
                UserId = _userContext.CurrentUser!.Id,
                CreatedDate = DateTime.UtcNow
            };

            dto.CartId = cartModel.Id;
        }

        await _lineRepository.Insert(_mapper.Map<CartLineModel>(dto));
        var cart = _mapper.Map<CartDTO>(await _repository.Get(dto.CartId));
        await CalculateAmounts(cart);
        return cart;
    }

    public async Task<CartDTO> Update(CartLineUpdateDTO dto)
    {
        var cartLine = await _lineRepository.Update(_mapper.Map<CartLineModel>(dto));
        return _mapper.Map<CartDTO>(_repository.Get(cartLine.CartId));
    }

    private async Task CalculateAmounts(CartDTO cart)
    {
        await _priceManager.SetDiscountedPrice(cart.Lines);
        if (cart.Lines is not null)
        {
            cart.Amount = await _priceManager.CalculateTotalDiscountedPrice(cart.Lines.Sum(q => q.Amount * q.Quantity));
        }
    }
}