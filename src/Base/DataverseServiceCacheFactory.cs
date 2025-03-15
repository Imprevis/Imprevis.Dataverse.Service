namespace Imprevis.Dataverse.Service;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Extensions.Caching.Hybrid;
using System;

internal class DataverseServiceCacheFactory(HybridCache hybridCache) : IDataverseServiceCacheFactory
{
    public IDataverseServiceCache Create(Guid organizationId)
    {
        return new DataverseServiceCache(hybridCache, organizationId);
    }
}
