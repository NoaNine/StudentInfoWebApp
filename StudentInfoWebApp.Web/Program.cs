using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.Core.Services;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL;
using StudentInfoWebApp.DAL.UnitOfWork;

namespace StudentInfoWebApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDatabase")));
        ConfigureServices(builder.Services);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

#if Development
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<UniversityContext>();

            if (!context.Groups.Any() && !context.Students.Any() && !context.Courses.Any())
            {
                DataSeeder.SeedCourses(context);
                DataSeeder.SeedGroups(context);
                DataSeeder.SeedStudents(context);
            }
        }
#endif

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddControllersWithViews();

        services.AddTransient<ICourseService, CourseService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<IStudentService, StudentService>();
    }
}
