using Grafitist.Common.Middleware;
using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;

using Microsoft.EntityFrameworkCore;

using Grafitist.Misc.Managers;

using Microsoft.AspNetCore.Authorization;
using Grafitist.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

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

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, HasLoggedInAuthorizationHandler>();

builder.Services.AddScoped<IPriceManager, PriceManager>();
builder.Services.AddScoped<IImageManager, ImageManager>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = TokenProvider.GetSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async c =>
                    {
                        c.HttpContext.Items["jwt-workaround"] = new Action(
                            async () =>
                            {
                                //Log exception or reason 
                                c.Response.ContentType = "plain/text";
                                c.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                const string authFailedMessage = "An error occurred processing your authentication.";
                                c.Response.Headers["auth-failed"] = new StringValues(authFailedMessage);

                                await c.Response.WriteAsync(authFailedMessage);
                            });

                        //c.Fail("An error occurred processing your authentication.");
                        await Task.CompletedTask;
                    },
                    OnTokenValidated = async t =>
                    {
                        t.HttpContext.Items["token-validated"] = true;

                        var payload = t.Principal!.Claims.FirstOrDefault(x => string.Compare(x.Type, "User", StringComparison.InvariantCultureIgnoreCase) == 0);
                        if (payload != null)
                        {
                            var user = JsonConvert.DeserializeObject<User>(payload.Value);
                            Console.WriteLine(user);
                            ((UserContext?)t.HttpContext.RequestServices.GetService<IUserContext>())!.CurrentUser = user;
                        }

                        await Task.CompletedTask;
                    }
                };
            });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.Admin, policy => policy.Requirements.Add(new AdminRequirement()));
    o.AddPolicy(Policies.User, policy => policy.Requirements.Add(new UserRequirement()));
    o.AddPolicy(Policies.HasLoggedIn, policy => policy.Requirements.Add(new HasLoggedInRequirement()));
});

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

//app.UseHttpsRedirection();
app.UseCors(corsOriginsPolicy);
app.UseJwtAuthentication();
app.UseConcurrentUserAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
