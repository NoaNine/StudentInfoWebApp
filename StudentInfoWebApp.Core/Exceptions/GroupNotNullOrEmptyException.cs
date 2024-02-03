namespace StudentInfoWebApp.Core.Exceptions;

internal class GroupNotNullOrEmptyException : Exception
{
    public GroupNotNullOrEmptyException()
    {

    }

    public GroupNotNullOrEmptyException(string message) 
        : base(message)
    {

    }

    public GroupNotNullOrEmptyException(string message, Exception inner) 
        : base(message, inner)
    {

    }
}
