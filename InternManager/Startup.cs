
using InternManager.Business.Abstract;
using InternManager.Business.Concrate;
using InternManager.DataAcces.Abstract;
using InternManager.DataAcces.Concrate;
using InternManager.Entities.Concrate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InternManager
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
            services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img")));

            services.AddTransient<IBossManager, BossManager>();
            services.AddTransient<IIntern1Manager, Intern1Manager>();
            services.AddTransient<IIntern2Manager, Intern2Manager>();
            services.AddTransient<IISEManager, ISEManager>();
            services.AddTransient<IPersonManager, PersonManager>();
            services.AddTransient<IStudentManager, StudentManager>();
            services.AddTransient<ITeacherManager, TeacherManager>();
            services.AddTransient<IFacultyManager, FacultyManager>();
            services.AddTransient<IKurumManager, KurumManager>();
            services.AddTransient<IInternManager, InternManagerer>();

            services.AddTransient<IBoosDal, EFBossDal>();
            services.AddTransient<IInternDal, EFInternDal>();
            services.AddTransient<IFacultyDal, EFFacultyDal>();
            services.AddTransient<IIntern1Dal, EFIntern1Dal>();
            services.AddTransient<IIntern2Dal, EFIntern2Dal>();
            services.AddTransient<IISEDal, EFISEDal>();
            services.AddTransient<IKurumDal, EFKurumDal>();
            services.AddTransient<IPersonDal, EFPersonDal>();
            services.AddTransient<IStudentDal, EFStudentDal>();
            services.AddTransient<ITeacherDal, EFTeacherDal>();



            services.AddDbContext<InternContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("InternManager.WebUI")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
