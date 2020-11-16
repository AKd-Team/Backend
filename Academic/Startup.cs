using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
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
            /*
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Administrators", policy => policy.RequireRole("Admin"));
                options.AddPolicy("admin", policy => policy.RequireClaim("tip", "admin"));
                options.AddPolicy("student", policy => policy.RequireClaim("tip", "student"));
                options.AddPolicy("profesor", policy => policy.RequireClaim("tip", "profesor"));
            });
            */
            //basic appsettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //se adauga user service-ul ce se va ocupa de servicii
            services.AddScoped<IUsersService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IProfesorService, ProfesorService>();
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