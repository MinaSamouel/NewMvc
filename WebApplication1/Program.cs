using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICourseRepo, CourseRepo>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IStudentLearningRepo, StudentLearningRepo>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StudentDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database1"));
            });

            builder.Services.AddMvc();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddScoped<IValidator<Student>, StudentValidator>();
            builder.Services.AddScoped<IValidator<Course>, CourseValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/404";
                    await next();

                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
