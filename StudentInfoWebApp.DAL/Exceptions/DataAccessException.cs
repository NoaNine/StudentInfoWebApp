namespace StudentInfoWebApp.DAL.Exceptions;

internal class DataAccessException : Exception
{
    internal DataAccessException()
    {

    }

    internal DataAccessException(string message)
        : base(message)
    {

    }

    internal DataAccessException(string message, Exception inner)
        : base(message, inner)
    {

    }
}
