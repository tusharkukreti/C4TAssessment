using C4TAssessment.BusinessAbstraction;
using C4TAssessment.BusinessImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace C4TAssessment.DI
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEnquiriesBusinessDomain, EnquiriesBusinessDomain>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
       
    }
}
