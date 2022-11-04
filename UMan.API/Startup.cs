using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UMan.Core.Repositories;
using UMan.DataAccess;
using UMan.DataAccess.Repositories;
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
            services.AddControllers();

            services.AddAutoMapper(c =>
            {
                c.AddProfile<PaperMapperProfile>();
                c.AddProfile<AuthorMapperProfile>();
                c.AddProfile<ArticleMapperProfile>();
            }, Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddDbContext<PapersDbContext>(o =>
            {
                o.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IRepository<Core.Paper>, PapersRepository>();

            services.AddScoped<IRepository<Core.Author>, AuthorsRepository>();

            services.AddRouting();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Papers API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Papers API");
            });

            app.UseRouting();

            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}