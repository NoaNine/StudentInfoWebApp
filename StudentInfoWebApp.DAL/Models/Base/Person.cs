﻿namespace StudentInfoWebApp.DAL.Models.Base;

public abstract class Person : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
