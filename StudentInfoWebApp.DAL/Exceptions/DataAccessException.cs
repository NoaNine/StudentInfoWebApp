﻿namespace StudentInfoWebApp.DAL.Exceptions;

internal class DataAccessException : Exception
{
    public DataAccessException()
    {

    }

    public DataAccessException(string message)
        : base(message)
    {

    }

    public DataAccessException(string message, Exception inner)
        : base(message, inner)
    {

    }
}
