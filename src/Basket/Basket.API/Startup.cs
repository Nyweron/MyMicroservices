using AutoMapper;
using Basket.API.Data;
using Basket.API.Data.interfaces;
using Basket.API.Entities;
using Basket.API.Mapper;
using Basket.API.Repositories;
using Basket.API.Repositories.interfaces;
using Basket.API.Settings;
using EventBusRabbitMq;
using EventBusRabbitMq.Producer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace Basket.API
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
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddTransient<IBasketContext, BasketContext>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(BasketMapper));

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<BasketCart>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Basket API" });
            });

            services.AddSingleton<IRabbitMqConnection>(sp =>
            {
                var rabbitMqSettings = Configuration.GetSection("EventBus").Get<RabbitmqSettings>();

                var factory = new ConnectionFactory
                {
                    UserName = rabbitMqSettings.Username,
                    HostName = rabbitMqSettings.Hostname,
                    Password = rabbitMqSettings.Password
                };

                return new RabbitMqConnection(factory);
            });

            services.AddSingleton(typeof(EventBusRabbitMqProducer));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API V1");
            });
        }
    }
}
