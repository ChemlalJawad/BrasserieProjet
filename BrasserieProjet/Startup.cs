using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BP.Data;
using BP.Data.Repositories;
using BP.Data.Repositories.Interfaces;
using BP.Service.Beers.Services;
using BP.Service.Beers.Services.Interfaces;
using BP.Service.Brewers.Services;
using BP.Service.Brewers.Services.Interfaces;
using BP.Service.Wholesalers.Services;
using BP.Service.Wholesalers.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BrasserieProjet
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
            services
              .AddControllers()
              .AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddDbContext<BrasserieContext>(options =>
            {
                options.UseSqlServer(@"Data Source=DESKTOP-5ARGQ5B\MSSQLSERVER01;Initial Catalog=localdb;Integrated Security=True");
            });


            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IBrewerRepository, BrewerRepository>();
            services.AddScoped<IWholesalerRepository, WholesalerRepository>();

            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBrewerService, BrewerService>();
            services.AddScoped<IWholesalerService, WholesalerService>();
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
