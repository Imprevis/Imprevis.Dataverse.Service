namespace Imprevis.Dataverse.Service.Exceptions;

using System;

public class DataverseServiceNotReadyException : Exception
{
    public DataverseServiceNotReadyException() : base("Service is not ready.")
    {
    }
}
