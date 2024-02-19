using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.Core.Services;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL;
using StudentInfoWebApp.DAL.UnitOfWork;
using StudentInfoWebApp.Web.Filters;
using System.Globalization;

namespace StudentInfoWebApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseRequestLocalization();

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

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new ExceptionFilter());
        });

        #region LocalizationConfigure
        services.AddTransient<SharedResource>();
        services.AddLocalization(option => option.ResourcesPath = "Resources");
        services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
            new CultureInfo("en"),
            new CultureInfo("uk")
            };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        #endregion

        services.AddDbContext<UniversityContext>(options => options.UseSqlServer(configuration.GetConnectionString("UniversityDatabase")));
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<ICourseService, CourseService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<IStudentService, StudentService>();
    }
}
