using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Academic.Helpers;
using Academic.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;
using Academic.Entities;

namespace Academic
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //adauga contextul basei de date
            services.AddDbContext<academicContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Db")));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //adauga corsul(chestia care decide daca te lasa sau nu sa faci request, trebuie dezvoltat
            services.AddCors();
            services.AddControllers();
            //basic appsettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //se adauga user service-ul ce se va ocupa de servicii
            services.AddScoped<UserService.IUsersService, UserService.UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                app.UseRouting();
                //se selecteaza ce poate face Cors-ul
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
                //basically meniul de "home" dar pt api-uri
                //aici putem selecta adresa pe care facem toate requesturile(in cazul actual /users/...
                app.UseMiddleware<JwtMiddleware>();
                //ce va inchide aplicatia?
                app.UseEndpoints(x => x.MapControllers());
            });
        }
    }
}