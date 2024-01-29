namespace StudentInfoWebApp.Core.Exceptions;

internal class GroupNotNullOrEmpty : Exception
{
    public GroupNotNullOrEmpty()
    {

    }

    public GroupNotNullOrEmpty(string message) 
        : base(message)
    {

    }

    public GroupNotNullOrEmpty(string message, Exception inner) 
        : base(message, inner)
    {

    }
}
