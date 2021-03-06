using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NegativeInfoService.Application.Interfaces;
using NegativeInfoService.Application.Services;
using NegativeInfoService.Domain.Interfaces;
using NegativeInfoService.Infra.Data.Repositories;

namespace NegativeInfoService.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            // Services classes
            services.AddScoped<INegationService, NegationService>();

            // Repositories classes
            services.AddScoped<INegationRepository, NegationRepository>();

            // Queue
            services.AddSingleton<IBureauNotificationQueue, AWSNotificationQueue>();

            // Amazon Web Service
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSimpleNotificationService>();
        }
    }
}
