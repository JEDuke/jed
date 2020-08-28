using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace jamesethanduke
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
                 {
                    options.DefaultScheme = options.DefaultAuthenticateScheme;
                    options.DefaultChallengeScheme = options.DefaultChallengeScheme;
                    //options.DefaultAuthenticateScheme = options.DefaultAuthenticateScheme;
                 })
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                })
                .AddMicrosoftAccount(options => 
                {
                    IConfigurationSection msftAuthNSection = Configuration.GetSection("Authentication:Microsoft");

                    options.ClientId = msftAuthNSection["ClientId"];
                    options.ClientSecret = msftAuthNSection["ClientSecret"];
                })
                .AddCookie();

            // services.AddAuthorization(options => 
            //     { 
            //         options.DefaultPolicy = options.DefaultPolicy;
            //     }
            // );
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
                app.UseExceptionHandler("/About/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=About}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "{controller=Error}/{action=500}/{message?}"
                );
            });
        }
    }
}
