using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // controllers
            services.AddControllers();
            
            // common
            services.AddScoped<IOrchestrator, MigrationOrchestrator>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITokenPayloadBuilder, TokenPayloadBuilder>();
            services.AddScoped<IEncoder, TokenEncoder>();
            
            // slots
            services.AddScoped<ISlotClient, SlotClient>();
            services.AddScoped<ISlotHttpClientWrapper, SlotHttpClientWrapper>();
            services.AddScoped<ISlotResponseDeserializer, SlotResponseDeserializer>();
            
            // add appointment
            services.AddScoped<IAddAppointmentClient, AddAppointmentClient>();
            services.AddScoped<IAddAppointmentHttpClientWrapper, AddAppointmentHttpClientWrapper>();
            services.AddScoped<IAddAppointmentRequestBuilder, AddAppointmentRequestBuilder>();
            services.AddScoped<IAddAppointmentRequestDeserializer, AddAppointmentRequestDeserializer>();
            services.AddScoped<IAddAppointmentResponseDeserializer, AddAppointmentResponseDeserializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
