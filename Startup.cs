using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Stripe;
using SPS.UI.Filters;
using SPS.UI.Service.Extensions;
using SPS.UI.Service.Extensions.EmailSender;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SPS.UI
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



            services.Configure<StripeModel>(Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe:SecretKey").Get<string>();

            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                option.Cookie.Name = ".SPS.Session";
                option.IdleTimeout = TimeSpan.FromMinutes(120);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });

#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            { return true; };
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
#endif


            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHttpRequestExtension, HttpRequestExtension>();
            services.AddTransient<IGridEmailSender, GridEmailSender>();


            services.Configure<RestSharpConfiguration>(Configuration.GetSection("HttpRequest"));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddOptions();
#if DEBUG
            services.AddControllersWithViews(otp=>
            {
                otp.Filters.Add<UnhandleExceptionFilterAttribute>();
            }).AddRazorRuntimeCompilation();
 #endif

            services.AddControllers().AddNewtonsoftJson();

            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
