using ecommerce.Core.Contexts;
using ecommerce.Core.Database;
using ecommerce.Core.Factories;
using ecommerce.Core.Factories.Interfaces;
using ecommerce.Core.MappingProfiles;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services;
using ecommerce.Core.Services.Interfaces;
using ecommerce.Core.Validators;
using ecommerce.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        private string _connectionString { get => Configuration["DatabaseName"]; }
        private DbConfig _dbConfig { get => new DbConfig(_connectionString); }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            
            services.AddMvc(o => 
                    o.Filters.Add(typeof(ModelStateValidator)))
                .AddFluentValidation(x => 
                    x.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());
            
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ecommerce", Version = "v1"});
            });
            
            services.AddRouting(o => o.LowercaseUrls = true);
            
            services.AddDbContext<ApplicationDbContext>(o => 
                o.UseSqlite(_connectionString));
            
            services.AddSingleton(_dbConfig);

            services.AddSingleton<IProductFactory,  ProductFactory>();
            services.AddSingleton<ICustomerFactory, CustomerFactory>();
            services.AddSingleton<IOrderFactory,    OrderFactory>();
            
            services.AddScoped<IProductRepository,  ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository,    OrderRepository>();

            services.AddScoped<IProductService,     ProductService>();
            services.AddScoped<ICustomerService,    CustomerService>();
            services.AddScoped<IOrderService,       OrderService>();
            
            
            services.AddScoped<IAnalyticsService,   AnalyticsService>();
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