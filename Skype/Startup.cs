using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Skype.Authorization;
using Skype.ChatService;
using Skype.Database;
using Skype.Models;
using Skype.ServiceModels;
using Skype.Services;
using System.Threading.Tasks;

namespace Skype
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile(new DataMapper());
            //});

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SkypeContext>()
                .AddDefaultTokenProviders();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SkypeContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserService>();


            // auth by cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Login");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSignalR().AddHubOptions<ChatHub>(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //    options.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           //.WithOrigins("http://localhost:4200");
                           .AllowAnyOrigin()
                            .AllowCredentials();
                });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.RequireHttpsMetadata = false;
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuer = true,
            //                ValidIssuer = AuthOptions.ISSUER,
            //                ValidateAudience = true,
            //                ValidAudience = AuthOptions.AUDIENCE,
            //                ValidateLifetime = true,
            //                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //                ValidateIssuerSigningKey = true,
            //            };
            //            options.Events = new JwtBearerEvents
            //            {
            //                OnMessageReceived = context =>
            //                {
            //                    var accessToken = context.Request.Query["access_token"];

            //                    // если запрос направлен хабу
            //                    var path = context.HttpContext.Request.Path;
            //                    if (!string.IsNullOrEmpty(accessToken) &&
            //                        (path.StartsWithSegments("/chat")))
            //                    {
            //                        // получаем токен из строки запроса
            //                        context.Token = accessToken;
            //                    }
            //                    return Task.CompletedTask;
            //                }
            //            };
            //        });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters =
                 new TokenValidationParameters
                 {
                     LifetimeValidator = (before, expires, token, parameters) => expires > System.DateTime.UtcNow,
                     ValidateAudience = false,
                     ValidateIssuer = false,
                     ValidateActor = false,
                     ValidateLifetime = true,
                     IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];

                         if (!string.IsNullOrEmpty(accessToken) &&
                             (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                         {
                             context.Token = context.Request.Query["access_token"];
                         }
                         return Task.CompletedTask;
                     }
                 };
             });

            services.AddSignalR();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //  services.AddAutoMapper();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                if (context.Request.Query.TryGetValue("token", out var token))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
                await next.Invoke();
            });

            app.UseDefaultFiles();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors("AllowAllOrigin");

            app.UseMiddleware<Middleware.WebSocketsMiddleware>();

            app.UseAuthentication();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(
                    "angular-fallback",
                    new { controller = "Account", action = "Login" });
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat",
                    options =>
                    {
                        options.ApplicationMaxBufferSize = 64;
                        options.TransportMaxBufferSize = 64;
                        options.LongPolling.PollTimeout = System.TimeSpan.FromMinutes(1);
                        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                    });
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
