using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using BL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DAL.models;

namespace final_project
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();



            services.AddScoped<ITelephonistBL, TelephonistBL>();
            services.AddScoped<ITelephonistDAL, TelephonistDAL>(); 

            services.AddScoped<ITelephonistCompaniesBL, TelephonistCompaniesBL>();
            services.AddScoped<ITelephonistCompaniesDAL, TelephonistCompaniesDAL>();

            services.AddScoped<ICallsBL, CallsBL>();
            services.AddScoped<ICallsDAL, CallsDAL>();

            services.AddScoped<IPhone_numbersBL, Phone_numbersBL>();
            services.AddScoped<IPhone_numbersDAL, Phone_numbersDAL>();

            services.AddScoped<IContributionBL, ContributionBL>();
            services.AddScoped<IContributionDAL, ContributionDAL>();
            
            services.AddScoped<ICompaniesBL, CompaniesBL>();
            services.AddScoped<ICompaniesDAL, CompaniesDAL>();

            services.AddDbContext<CUSERS1DESKTOPDATABASEMARKETINMDFContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\1\Desktop\Tele-marketing.mdf;Integrated S
ecurity=True;Connect Timeout=30"), ServiceLifetime.Scoped);
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                    builder.WithHeaders("Content-Type");
                    builder.WithMethods(new String[] { "GET", "POST", "PUT", "DELETE" });
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            // app.UseMvc();

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
