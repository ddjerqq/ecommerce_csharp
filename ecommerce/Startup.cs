using System;
using System.Text.Json.Serialization;
using ecommerce.Core.Contexts;
using ecommerce.Core.Database;
using ecommerce.Core.Profiles;
using ecommerce.Core.Repositories;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services;
using ecommerce.Core.Services.Interfaces;
using ecommerce.Core.Validators;
using ecommerce.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ecommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            
            services.AddMvc(o => 
                    o.Filters.Add(typeof(ModelStateValidator)))
                .AddFluentValidation(x => 
                    x.RegisterValidatorsFromAssemblyContaining<UserValidator>());
            
            services.AddControllers()
                .AddJsonOptions(o => {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ecommerce", Version = "v1"});
            });
            
            services.AddRouting(o => o.LowercaseUrls = true);

            // services.AddDbContext<ApplicationDbContext>(options =>
            //     {
            //         options.UseSqlite(
            //             "Data Source=C:\\work\\!actual_work\\ecommerce_api\\ecommerce\\ecommerce.Core\\database.sqlite"
            //         );
            //     });

            string conStr = Configuration["DatabaseName"];
            DbConfig dbConfig = new DbConfig {ConnectionString = conStr};;
            services.AddSingleton(dbConfig);
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddSingleton<IUserService,    UserService>();
        }

        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ecommerce v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}