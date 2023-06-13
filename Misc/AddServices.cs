using Grafitist.Repositories.Campaign;
using Grafitist.Repositories.Campaign.Interfaces;
using Grafitist.Repositories.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Grafitist.Repositories.Order.Interfaces;
using Grafitist.Repositories.Order;
using Grafitist.Repositories.User;
using Grafitist.Repositories.User.Interfaces;
using Grafitist.Repositories.Stock;
using Grafitist.Repositories.Payment.Interfaces;
using Grafitist.Repositories.Payment;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Repositories.Product;
using Grafitist.Repositories.Stock.Interfaces;
using Grafitist.Repositories.CompanyInfo.Interfaces;
using Grafitist.Repositories.CompanyInfo;
using Grafitist.Services.Campaign.Interfaces;
using Grafitist.Services.Campaign;
using Grafitist.Services.CartService;
using Grafitist.Services.Order;
using Grafitist.Services.Payment;
using Grafitist.Services.Product;
using Grafitist.Services.Stock;
using Grafitist.Services.Cart.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Grafitist.Services.Payment.Interfaces;
using Grafitist.Services.Product.Interfaces;
using Grafitist.Services.Stock.Interfaces;
using Grafitist.Services.CompanyInfo.Interfaces;
using Grafitist.Services.CompanyInfo;
using Grafitist.Services.User;
using Grafitist.Services.User.Interfaces;

namespace Grafitist.Misc;

public static class ContextExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartLineRepository, CartLineRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderLineRepository, OrderLineRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<IMaterialRepository, MaterialRepository>();
        services.AddScoped<IVariantRepository, VariantRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IAddressDataRepository, AddressDataRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<IMaterialService, MaterialService>();
        services.AddScoped<IVariantService, VariantService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<IAddressDataService, AddressDataService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyInfoService, CompanyInfoService>();

        return services;
    }
}