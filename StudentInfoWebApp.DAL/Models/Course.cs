using StudentInfoWebApp.DAL.Models.Base;

namespace StudentInfoWebApp.DAL.Models;

public class Course : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Group> Groups { get; set; }
}
