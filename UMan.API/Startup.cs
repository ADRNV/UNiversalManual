using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using UMan.API.Middlewares;
using UMan.Core.Repositories;
using UMan.DataAccess;
using UMan.DataAccess.Entities;
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
            services.AddDbContext<ApiUsersContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("ApiUserDbConnection"));
            })
                .AddDefaultIdentity<User>(options =>
                {

                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;

                }).AddEntityFrameworkStores<ApiUsersContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<PapersDbContext>(o =>
            {
                o.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });

            var jwtTokenOptions = _config.GetSection("jwtTokenOptions")
                .Get<JwtTokenOptions>();

            services.AddSingleton(jwtTokenOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtTokenOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenOptions.Secret)),
                        ValidAudience = jwtTokenOptions.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

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

            services.AddScoped<IRepository<Core.Paper>, PapersRepository>();

            services.AddScoped<IPapersRepository, PapersRepository>();

            services.AddScoped<IRepository<Core.Author>, AuthorsRepository>();

            services.AddRouting();

            services.AddAuthentication();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IJwtAuthManager, JwtAuthManager>();

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
                c.SupportNonNullableReferenceTypes();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
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

            app.UseAuthentication();

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