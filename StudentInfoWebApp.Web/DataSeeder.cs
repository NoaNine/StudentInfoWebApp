using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Web;

internal class DataSeeder
{
    public static void SeedCourses(DbContext context)
    {
        var courses = new List<Course>
            {
                new Course { Name = "Прикладна математика", Description = "" },
                new Course { Name = "Комп`ютерна інженерія", Description = "" },
                new Course { Name = "Електроніка та електромеханіка", Description = "" },
                new Course { Name = "Юридичне право", Description = "" }
            };
        context.AddRange(courses);
        context.SaveChanges();
    }

    public static void SeedGroups(DbContext context)
    {
        var groups = new List<Group>
            {
                new Group { CourseId = 1, Name = "SR-11" },
                new Group { CourseId = 1, Name = "SR-12" },
                new Group { CourseId = 1, Name = "SR-13" },
                new Group { CourseId = 2, Name = "PI-21" },
                new Group { CourseId = 3, Name = "EE-31" },
                new Group { CourseId = 4, Name = "YP-41" }
            };
        context.AddRange(groups);
        context.SaveChanges();
    }

    public static void SeedStudents(DbContext context)
    {
        var students = new List<Student>
            {
                new Student { GroupId = 1, FirstName = "Ія", LastName = "Атрощенко" },
                new Student { GroupId = 1, FirstName = "Гаїна", LastName = "Троцька" },
                new Student { GroupId = 1, FirstName = "Устина", LastName = "Глущак" },
                new Student { GroupId = 1, FirstName = "Шанетта", LastName = "Морачевська" },
                new Student { GroupId = 1, FirstName = "Уляна", LastName = "Савула" },
                new Student { GroupId = 1, FirstName = "Глафира", LastName = "Шамрай" },
                new Student { GroupId = 1, FirstName = "Корнелія", LastName = "Магура" },
                new Student { GroupId = 1, FirstName = "Фелікса", LastName = "Коник" },
                new Student { GroupId = 1, FirstName = "Улита", LastName = "Фартушняк" },
                new Student { GroupId = 1, FirstName = "Ада", LastName = "Варивода" },
                new Student { GroupId = 1, FirstName = "Есфіра", LastName = "Мороз" },
                new Student { GroupId = 2, FirstName = "Йоган", LastName = "Рижук" },
                new Student { GroupId = 2, FirstName = "Вітан", LastName = "Боровий" },
                new Student { GroupId = 2, FirstName = "Яртур", LastName = "Жук" },
                new Student { GroupId = 2, FirstName = "Славобор", LastName = "Сливенко" },
                new Student { GroupId = 2, FirstName = "Кий", LastName = "Бузинний" },
                new Student { GroupId = 2, FirstName = "Дантур", LastName = "Горовенко" },
                new Student { GroupId = 2, FirstName = "Ярчик", LastName = "Чічка" },
                new Student { GroupId = 3, FirstName = "Матвій", LastName = "Білявський" },
                new Student { GroupId = 3, FirstName = "Недан", LastName = "Баліцький" },
                new Student { GroupId = 3, FirstName = "Щек", LastName = "Удовенко" },
                new Student { GroupId = 3, FirstName = "Орест", LastName = "Колосовський" },
                new Student { GroupId = 3, FirstName = "Йонас", LastName = "Вихрущ" },
                new Student { GroupId = 3, FirstName = "Наслав", LastName = "Прокопчук" },
                new Student { GroupId = 3, FirstName = "Куйбіда", LastName = "Лемешко" },
                new Student { GroupId = 3, FirstName = "Ліпослав", LastName = "Мовчан" },
                new Student { GroupId = 3, FirstName = "Снозір", LastName = "Назарук" },
                new Student { GroupId = 4, FirstName = "Дорогосил", LastName = "Тарасович" },
                new Student { GroupId = 4, FirstName = "Юхим", LastName = "Забродський" },
                new Student { GroupId = 4, FirstName = "Яртур", LastName = "Цвєк" },
                new Student { GroupId = 4, FirstName = "Лук`ян", LastName = "Григоренко" },
                new Student { GroupId = 4, FirstName = "Хорив", LastName = "Горбачевський" },
                new Student { GroupId = 4, FirstName = "Царко", LastName = "Киричук" },
                new Student { GroupId = 4, FirstName = "Творимир", LastName = "Яхненко" },
                new Student { GroupId = 4, FirstName = "Яснолик", LastName = "Рошко" },
                new Student { GroupId = 4, FirstName = "Живорід", LastName = "Керножицький" },
                new Student { GroupId = 4, FirstName = "Нестор", LastName = "Засядько" },
                new Student { GroupId = 4, FirstName = "Йомер", LastName = "Павличенко" },
                new Student { GroupId = 4, FirstName = "Малик", LastName = "Білоскурський" },
                new Student { GroupId = 4, FirstName = "Осемрит", LastName = "Синиця" },
                new Student { GroupId = 5, FirstName = "Явір", LastName = "Сливенко" },
                new Student { GroupId = 5, FirstName = "Колодар", LastName = "Гайдабура" },
                new Student { GroupId = 5, FirstName = "Макар", LastName = "Гембицький" },
                new Student { GroupId = 5, FirstName = "Радогоста", LastName = "Гаркуша" },
                new Student { GroupId = 5, FirstName = "Юдихва", LastName = "Степура" },
                new Student { GroupId = 5, FirstName = "Млада", LastName = "Сенько" },
                new Student { GroupId = 6, FirstName = "Римма", LastName = "Пашко" },
                new Student { GroupId = 6, FirstName = "Цвітана", LastName = "Могиленко" },
                new Student { GroupId = 6, FirstName = "Марта", LastName = "Кирей" },
                new Student { GroupId = 6, FirstName = "Глафіра", LastName = "Любенецька" },
                new Student { GroupId = 6, FirstName = "Віра", LastName = "Тарасовна" },
                new Student { GroupId = 6, FirstName = "Жадана", LastName = "Заяць" },
                new Student { GroupId = 6, FirstName = "Тава", LastName = "Андрусенко" },
                new Student { GroupId = 6, FirstName = "Ядвіга", LastName = "Воронюк" },
                new Student { GroupId = 6, FirstName = "Стелла", LastName = "Рибенчук" },
                new Student { GroupId = 6, FirstName = "Мокрина", LastName = "Трегуб" }
            };
        context.AddRange(students);
        context.SaveChanges();
    }
}
