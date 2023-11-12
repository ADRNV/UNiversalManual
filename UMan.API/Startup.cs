using FeedParser.Parsers.Updates.Schedulers;
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
using UMan.API.StartupExtensions;
using UMan.Core.Repositories;
using UMan.Core.Services;
using UMan.DataAccess;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Repositories;
using UMan.DataAccess.Security;
using UMan.DataAccess.Security.Common;
using UMan.Domain;
using UMan.Services.Parsing;

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
            services.AddIdentity(_config);

            services.AddDbContext<PapersDbContext>(o =>
            {
                o.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });

            services.AddJwtAuthentication(_config);

            services.AddControllers();

            services.AddAutoMapper(c =>
            {
                c.AddProfile<PaperMapperProfile>();
                c.AddProfile<AuthorMapperProfile>();
                c.AddProfile<ArticleMapperProfile>();
                c.AddProfile<HashTagMapperProfile>();
            }, Assembly.GetExecutingAssembly());

            services.AddMediatrRequerments();

            services.AddRepositories();

            services.AddRouting();

            services.AddMvc();

            services.AddSwaggerDoc();

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