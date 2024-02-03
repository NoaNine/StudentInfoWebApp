namespace StudentInfoWebApp.Core.Exceptions;

internal class CourseNotNullOrEmptyException : Exception
{
    public CourseNotNullOrEmptyException()
    {

    }

    public CourseNotNullOrEmptyException(string message)
        : base(message)
    {

    }

    public CourseNotNullOrEmptyException(string message, Exception inner)
        : base(message, inner)
    {

    }
}