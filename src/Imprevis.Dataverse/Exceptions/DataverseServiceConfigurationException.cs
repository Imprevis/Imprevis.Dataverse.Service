namespace Imprevis.Dataverse.Exceptions;

using System;

public class DataverseServiceConfigurationException : Exception
{
    public DataverseServiceConfigurationException() : base("Dataverse service configuration does not match connected service.")
    {
    }
}
