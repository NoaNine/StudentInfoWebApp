using StudentInfoWebApp.DAL.Models.Base;

namespace StudentInfoWebApp.DAL.Models;

public class Student : Person
{
    public int GroupId { get; set; }
    public Group Group { get; set; }
}
