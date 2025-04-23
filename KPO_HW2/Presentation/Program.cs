using KPO_HW2.Application.EventHandlers;
using KPO_HW2.Application.Services;
using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Service;
using KPO_HW2.Infrastructure.Repositories;
using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using KPO_HW2.Domain.Factories;
using KPO_HW2.Domain.FactoriesInterfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
namespace KPO_HW2.Presentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Репозитории (Infrastructure)
            builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
            builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
            builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

            // Сервисы (Application)
            builder.Services.AddScoped<IAnimalTransferService, AnimalTransferService>();
            builder.Services.AddScoped<IZooStatisticsService, ZooStatisticsService>();
            builder.Services.AddHostedService<FeedingOrganizationService>();

            // Регистрация фабрик
            builder.Services.AddScoped<IAnimalFactory, AnimalFactory>();
            builder.Services.AddScoped<IEnclosureFactory, EnclosureFactory>();
            builder.Services.AddScoped<IFeedingScheduleFactory, FeedingScheduleFactory>();

            builder.Services.AddSwaggerGen(c =>
            {
                // Включите XML-документацию
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(
                        namingPolicy: JsonNamingPolicy.CamelCase,  
                        allowIntegerValues: false                  
                    ));
            });

            // MediatR (если используется)
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // === Построение приложения ===
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization(); 
            app.MapControllers();
            app.Run();
        }
    }
}
