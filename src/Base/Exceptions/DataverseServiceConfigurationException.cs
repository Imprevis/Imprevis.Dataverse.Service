namespace Imprevis.Dataverse.Service.Exceptions;

using System;

public class DataverseServiceConfigurationException(string message) : Exception(message)
{
}
