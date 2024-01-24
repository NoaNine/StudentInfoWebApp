using StudentInfoWebApp.DAL.Models.Base;

namespace StudentInfoWebApp.DAL.Models;

public class Group : BaseModel
{
    public string Name { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public ICollection<Student> Students { get; set; } 
}
