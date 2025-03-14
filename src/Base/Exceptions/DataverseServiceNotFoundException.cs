namespace Imprevis.Dataverse.Exceptions;

using System;

public class DataverseServiceNotFoundException : Exception
{
    public DataverseServiceNotFoundException() : base("Service was not found.")
    {
    }
}
