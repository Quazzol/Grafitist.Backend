using Grafitist.Common.Middleware;
using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;
using Grafitist.Repositories.Campaign;
using Grafitist.Repositories.Campaign.Interfaces;
using Grafitist.Repositories.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Grafitist.Repositories.Order.Interfaces;
using Grafitist.Repositories.Order;
using Grafitist.Repositories.User;
using Grafitist.Repositories.User.Interfaces;
using Grafitist.Services.User;
using Grafitist.Services.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using Grafitist.Repositories.Stock;
using Grafitist.Repositories.Payment.Interfaces;
using Grafitist.Repositories.Payment;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Repositories.Product;
using Grafitist.Repositories.Stock.Interfaces;
using Grafitist.Services.Campaign.Interfaces;
using Grafitist.Services.Campaign;
using Grafitist.CartService.Services;
using Grafitist.Services.Order;
using Grafitist.Services.Payment;
using Grafitist.Services.Product;
using Grafitist.Services.Stock;
using Grafitist.Services.Cart.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Grafitist.Services.Payment.Interfaces;
using Grafitist.Services.Product.Interfaces;
using Grafitist.Services.Stock.Interfaces;
using Grafitist.Misc.Managers;

var builder = WebApplication.CreateBuilder(args);
var corsOriginsPolicy = "CorsOrigins";
var connectionString = builder.Configuration.GetConnectionString("SqlConnection")!;

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOriginsPolicy,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["AllowedCorsHosts"] ?? string.Empty).AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartLineRepository, CartLineRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IVariantRepository, VariantRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IAddressDataRepository, AddressDataRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IVariantService, VariantService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IAddressDataService, AddressDataService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPriceManager, PriceManager>();
builder.Services.AddScoped<IImageManager, ImageManager>();
builder.Services.AddScoped<IPriceManager, PriceManager>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsOriginsPolicy);
app.UseConcurrentUserAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
