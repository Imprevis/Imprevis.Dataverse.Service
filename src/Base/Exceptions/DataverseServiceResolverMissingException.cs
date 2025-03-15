namespace Imprevis.Dataverse.Service.Exceptions;

using System;

public class DataverseServiceResolverMissingException : Exception
{
    public DataverseServiceResolverMissingException() : base("At least one resolver must be added.")
    {
    }
}
