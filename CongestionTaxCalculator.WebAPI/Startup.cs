using CongestionTaxCalculator.Services;
using CongestionTaxCalculator.WebAPI.Controllers;
using CongestionTaxCalculator.WebAPI.Respositories;
using CongestionTaxCalculator.WebAPI.Respositories.Implementation;
using CongestionTaxCalculator.WebAPI.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace CongestionTaxCalculator.WebAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CongestionTaxCalculator.WebAPI", Version = "v1" });
            });

            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IDbConnection>(c => new SqlConnection(connectionString));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole(); 
                loggingBuilder.AddDebug();
            });
            services.AddScoped<ICongestionTaxService, CongestionTaxService>();
            services.AddScoped<ICongestionTaxRepository, CongestionTaxRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
     
                app.UseSwagger();
                
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CongestionTaxCalculator.WebAPI V1");
                });
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
