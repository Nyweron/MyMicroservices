using EventBusRabbitMq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ordering.API.RabbitMq;
using Ordering.API.Settings;
using Ordering.Application.Handlers.QueryHandlers;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using RabbitMQ.Client;

namespace Ordering.API
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
            services.AddDbContext<OrderContext>(
                options => options.UseSqlServer("name=ConnectionStrings:OrderConnection",
                providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Order API" });
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

            services.AddSingleton(typeof(EventBusRabbitMqConsumer));

            services.AddAutoMapper(typeof(OrderMapper));

            services.AddMediatR(typeof(GetOrderByUserNameHandler));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
            });
        }
    }

}

