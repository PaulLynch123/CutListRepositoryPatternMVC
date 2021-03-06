using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
/*not needed as moved the data inside DataAccess project
using CutListRepositoryPatternMVC.Data;
*/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//replaced the removed one above
using CutList.DataAccess.Data;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using CutList.Utility;
using CutList.DataAccess.Data.Initializer;

namespace CutListRepositoryPatternMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Configures dependency injection
        //Build on the services to use in our project
        public void ConfigureServices(IServiceCollection services)
        {
            //moved this file inside DataAccess
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
           //using the default connection string appsettings.json
                    Configuration.GetConnectionString("DefaultConnection")));
            /*when we register for account it will ask for confirmation
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            */
            //remove the above functionality for this application
            /* not using the default Identity, instead pass in the IdentityRole because we may want to change the Identity user
             * the default does not allow this
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                */
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //two factor authentication OR tocken use (forgetting password). 
                //this would have been included in the AddDefaultIdentity above but I edited that code
                .AddDefaultTokenProviders();
            //default UI will be included from Bootstrap 4 in version Core 3.0 onwards    


            //add this to implement the IEmailSender despite not doing anything with the Register email option
            services.AddSingleton<IEmailSender, EmailSender>();


            //add unitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Add my seeding Initialiser class
            services.AddScoped<IDbInitialiser, DbInitialiser>();

            //configure the session for use in 'Shopping Cart'
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //I have included RazorRuntimeCOmplication neGet package (MVC)
            //.AddNewtonsoftJson() for calling APIs use this Json object
            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            //for login pages access??????????
            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-3.0&tabs=visual-studio#create-full-identity-ui-source
            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = $"/Identity/Account/Login";

                options.LogoutPath = $"/Identity/Account/Logout";

                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Middleware pipeline for request and response (context pipeline1 then pipeline2 etc response or 404 no response. Context Object goes back)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitialiser paulInitialise)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //use sessions for Shopping cart
            app.UseSession();

            app.UseRouting();

            //seed with admin user if not already present
            paulInitialise.Initialise();

            app.UseAuthentication();
            app.UseAuthorization();

            //sessions middleware

            //default pattern
            //conventional or endPoint(we inject middleware)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //included Area with default of Factory folder/user
                    pattern: "{area=Factory}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
