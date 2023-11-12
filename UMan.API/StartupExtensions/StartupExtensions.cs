using FeedParser.Parsers.Updates.Schedulers;
using Microsoft.Extensions.Hosting;
using FeedParser.Extensions;
using FeedParser.Core;
using FeedParser.Core.Models;
using UMan.DataAccess.Parsing.Handlers;
using UMan.Core.Repositories;
using FeedParser.Parsers.Updates.Schedulers.Configuration;
using UMan.DataAccess.Repositories;
using MediatR;
using System.Reflection;
using UMan.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using UMan.Domain;
using System.Security.Claims;
using UMan.DataAccess.Security.Common;
using FeedArticle = FeedParser.Core.Models.Article;
using UMan.Core.Services;
using UMan.Services.Parsing;
using Microsoft.OpenApi.Models;

namespace UMan.API.StartupExtensions
{
    public static class StartupExtensions
    {
        public static IHostBuilder ConfigureParserService(this IHostBuilder builder)
        {
            builder.ConfigureParsers();

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(s => new SchedulerOptions { UpdatesInterval = TimeSpan.FromSeconds(3) });

                services.AddTransient<IUpdateHandler<IEnumerable<FeedArticle>>, UpdateHandler>(sp =>
                {
                    var scope = sp.CreateScope();

                    var repository = scope.ServiceProvider.GetRequiredService<IPapersRepository>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<UpdateHandler>>();
                    return new UpdateHandler(repository, logger);
                });


                services.AddHostedService<UpdateScheduler>();
            });

            return builder;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiUsersContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApiUserDbConnection"));
            })
               .AddDefaultIdentity<User>(options =>
               {

                   options.SignIn.RequireConfirmedAccount = false;
                   options.Password.RequireDigit = false;
                   options.Password.RequiredLength = 6;
                   options.SignIn.RequireConfirmedEmail = false;

               }).AddRoles<UserRole>()
               .AddEntityFrameworkStores<ApiUsersContext>()
               .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenOptions = configuration.GetSection("jwtTokenOptions")
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

            services.AddAuthentication();

            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<IJwtAuthManager, JwtAuthManager>();

            services.AddScoped<IParsingServiceDispatcher<UpdateScheduler>, ParsingServiceDispatcher<UpdateScheduler>>(s =>
            {
                return new ParsingServiceDispatcher<UpdateScheduler>(s);
            });

            services.AddAuthorization(c =>
            {
                c.AddPolicy("User", buider =>
                {
                    buider.RequireClaim(ClaimTypes.Role, "User");
                });

                c.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                c.AddPolicy("Moder", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Moder");
                });

            });

            return services;
        }

        public static IServiceCollection AddMediatrRequerments(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Core.Paper>, PapersRepository>();

            services.AddScoped<IPapersRepository, PapersRepository>();

            services.AddScoped<IRepository<Core.Author>, AuthorsRepository>();

            return services;
        }

        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
