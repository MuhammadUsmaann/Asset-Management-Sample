using AssetManagement.Server.Infrastructure.BusinessLogic.Services;
using AssetManagement.Server.Infrastructure.DB.Context;
using AssetManagement.Server.Infrastructure.DB.Models;
using AssetManagement.Server.Infrastructure.Filters;
using AssetManagement.Server.Infrastructure.Helpers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;

using System.Reflection;
using System.Text;

namespace ServicesDataLayer.Infrastructure.Extensions
{
    public static class StartupExt
    {
        public static void AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AssetManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                b => Assembly.GetExecutingAssembly().ToString());
            });

            services.AddIdentity<UserProfile, IdentityRole<int>>(options =>
            {
                        options.Password.RequiredLength = 8;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<AssetManagementDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IIdentityService, IdentityService>();
            services.Configure<WebAppSettings>(configuration);

            services.AddScoped(typeof(IBaseServices<>), typeof(BaseService<>));
        }

       

        public static void AuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                };
            });
        }

        public static void ControllerConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews(x =>
            {
                x.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }

        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            services.AddEndpointsApiExplorer();
        }

        public static void CorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        public async static Task MigrateDatabaseAsync(this IApplicationBuilder host, CancellationToken cancellationToken = default)
        {
            using (var scope = host.ApplicationServices.CreateScope())
            {
                try
                {
                    Console.WriteLine("Migrate Db");

                    var dbcontext = scope.ServiceProvider.GetRequiredService<AssetManagementDbContext>();
                    
                    await dbcontext.Database.MigrateAsync(cancellationToken);
                    
                    Console.WriteLine("Migration Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

