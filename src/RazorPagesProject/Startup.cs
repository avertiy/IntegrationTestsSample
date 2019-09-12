using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorPagesProject.Services;

namespace RazorPagesProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // use different database provider then the integration tests
            var contentRoot = Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var connectionString = $"Data Source={contentRoot}{Path.DirectorySeparatorChar}database.db";
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString)
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            #region snippet1
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/SecurePage");
                });
            #endregion

            services.AddHttpClient<IGithubClient, GithubClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Yolo", "0.1.0"));
            });

            #region snippet2
            services.AddScoped<IQuoteService, QuoteService>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
			app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
