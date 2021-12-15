using C4TAssessment.DI;
using C4TAssessment.Models.Enquiries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace C4TAssessment.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customs4Trade Enquiries", Version = "v1" });
            });
            DependencyContainer.RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "C4TAssessment.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseExceptionHandler(
                     options =>
                     {
                         options.Run(
                             async context =>
                             {
                                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                 context.Response.ContentType = "text/html";
                                 var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                                 if (null != exceptionObject)
                                 {
                                     var errorItem = JsonConvert.SerializeObject
                                         (new ErrorItem
                                         (
                                             context.Response.StatusCode,
                                             exceptionObject.Error.Message,
                                             exceptionObject.Error.StackTrace)
                                         );
                                     logger.LogError(errorItem);
                                     await context.Response.WriteAsync
                                     (errorItem).ConfigureAwait(false);
                                 }
                             });
                     }
                 );
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
