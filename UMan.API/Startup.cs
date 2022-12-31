using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System.Reflection;
using System.Security.Claims;
using UMan.API.Middlewares;
using UMan.Core.Repositories;
using UMan.DataAccess;
using UMan.DataAccess.Repositories;
using UMan.DataAccess.Security;
using UMan.DataAccess.Security.Common;
using UMan.Domain;

namespace UMan.API
{
#pragma warning disable CS1591 
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiUsersContext>(o =>
            {
                o.UseSqlServer(_config.GetConnectionString("ApiUserDbConnection"));
            });

            services.AddDefaultIdentity<IdentityUser>(options =>
            {

                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

            }).AddEntityFrameworkStores<ApiUsersContext>();

            services.AddControllers();

            services.AddAutoMapper(c =>
            {
                c.AddProfile<PaperMapperProfile>();
                c.AddProfile<AuthorMapperProfile>();
                c.AddProfile<ArticleMapperProfile>();
                c.AddProfile<HashTagMapperProfile>();
            }, Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddDbContext<PapersDbContext>(o =>
            {
                o.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IRepository<Core.Paper>, PapersRepository>();

            services.AddScoped<IPapersRepository, PapersRepository>();

            services.AddScoped<IRepository<Core.Author>, AuthorsRepository>();

            services.AddRouting();

            services.AddAuthentication();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var jwtTokenOptions = _config.GetSection("jwtTokenOptions").Get<JwtTokenOptions>();

            services.AddAuthorization(c =>
            {
                c.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                c.AddPolicy("Moder", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Moder");
                });

            });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                c.SupportNonNullableReferenceTypes();

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {   new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()}
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Papers API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Papers API");
            });

            app.UseRouting();

            app.UseSwagger();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[".AspNetCore.Application.Id"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }

                await next();
            });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}