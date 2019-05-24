using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ateliware.RepositoriosDestaques.Application.Services;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using Ateliware.RepositoriosDestaques.Domain.Services;
using Ateliware.RepositoriosDestaques.DomainServices;
using Ateliware.RepositoriosDestaques.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ateliware.RepositoriosDestaques.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Dependency Injection
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IDestaquesApplicationService, DestaquesApplicationService>();
            services.AddScoped<IImportadorGitHubService, ImportadorGitHubService>();
            services.AddScoped<IDestaquesRepository, DestaquesRepository>();
            services.AddScoped<IHistoricoImportacaoRepository, HistoricoImportacaoRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Destaques}/{action=Index}/{id?}");
            });
        }
    }
}
