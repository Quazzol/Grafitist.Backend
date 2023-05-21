using AutoMapper;
using Grafitist.Contracts.Cart.Request;
using Grafitist.Contracts.Cart.Response;
using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Grafitist.Services.Cart.Interfaces;

namespace Grafitist.CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly ICartLineRepository _lineRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly IPriceManager _priceManager;

    public CartService(ICartRepository repository, ICartLineRepository lineRepository, IUserContext userContext, IMapper mapper, IPriceManager priceManager)
    {
        _repository = repository;
        _lineRepository = lineRepository;
        _userContext = userContext;
        _mapper = mapper;
        _priceManager = priceManager;
    }

    public async Task ApproveCart(Guid id)
    {
        await _repository.Update(new CartModel { Id = id, Status = CartStatus.Completed });
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

    public async Task<IEnumerable<CartDTO>> GetByUserId(Guid id, Pager pager)
    {
        var carts = _mapper.Map<IEnumerable<CartDTO>>(await _repository.GetByUser(id, pager));
        foreach (var cart in carts)
        {
            await CalculateAmounts(cart);
        }
        return carts;
    }

    public async Task<CartDTO> InsertLine(CartLineInsertDTO dto)
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

            await _repository.Insert(cartModel);
            dto.CartId = cartModel.Id;
        }

        await _lineRepository.Insert(_mapper.Map<CartLineModel>(dto));
        return _mapper.Map<CartDTO>(await _repository.Get(dto.CartId));
    }

    public async Task<CartDTO> UpdateLine(CartLineUpdateDTO dto)
    {
        var cartLine = await _lineRepository.Update(_mapper.Map<CartLineModel>(dto));
        return _mapper.Map<CartDTO>(_repository.Get(cartLine.CartId));
    }

    private async Task CalculateAmounts(CartDTO cart)
    {
        await _priceManager.CalculateDiscountedPrices(cart.Lines);
        if (cart.Lines is not null)
        {
            cart.Amount = await _priceManager.CalculateTotalDiscountedPrice(cart.Lines.Sum(q => q.Amount * q.Quantity));
        }
    }
}