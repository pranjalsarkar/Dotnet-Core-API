using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Core.WebAPI.Models;
using Core.WebAPI.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Core.WebAPI
{ 
    public class Startup
    {
        public Startup(IConfiguration configuration)
        { 
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db_ABC")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            
            //PS: Add CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:4200", "http://localhost:4300", "http://10.11.130.168").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddScoped<ISave, Main>();
            services.AddScoped<IUpdate, Main>();

            //Map client configuration type/contract to its implementation
            services.AddScoped<IClientConfiguration, ClientConfiguration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //PS: this is where we configure the built-in middleware or custom middleware.
            //Middlewares needs to be in order for some

            //custom logger middleware
            //app.UsePSLogger();
            //custom client configuration middleware
            //app.UseClientConfiguration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //logger.AddProvider(new LoggerDatabaseProvider(Configuration.GetConnectionString("db_ABC")));

            //PS: configure app to use CORS policy defined earlier
            app.UseCors(MyAllowSpecificOrigins);

            //static files and default files
            //app.UseDefaultFiles(); PS: this out-of-the-box middleware serves default files (files named as - default.html/htm or idex.html/htm)
            //that should be present in wwwroot folder of the application. This basically maps the default file (if any) and then it is served by the
            //useStaticFiles() middleware as this middleware only serves static files - html/css/images/js

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
