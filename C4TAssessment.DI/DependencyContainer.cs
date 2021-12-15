using C4TAssessment.BusinessAbstraction;
using C4TAssessment.BusinessImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace C4TAssessment.DI
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEnquiriesBusinessDomain, EnquiriesBusinessDomain>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAzureService, AzureService>();
            services.AddSingleton<IQueueClient>(x => new QueueClient
            (
                configuration["CloudConfig:QueueConnectionString"],
                configuration["CloudConfig:QueueName"])
            );
        }

    }
}
