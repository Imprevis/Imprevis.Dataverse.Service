namespace Imprevis.Dataverse.Exceptions;

using System;

public class DataverseServiceNotResolvedException : Exception
{
    public DataverseServiceNotResolvedException() : base("Service was not resolved.")
    {
    }
}
