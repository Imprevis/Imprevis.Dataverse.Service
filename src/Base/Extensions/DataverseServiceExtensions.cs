namespace Imprevis.Dataverse.Extensions;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public static class DataverseServiceExtensions
{
    public static async Task<Entity?> RetrieveSingleAsync(this IDataverseService service, QueryBase query, CancellationToken cancellationToken = default)
    {
        var results = await service.RetrieveMultipleAsync(query, cancellationToken);

        if (results.Entities.Count == 0)
        {
            return null;
        }

        if (results.Entities.Count == 1)
        {
            return results.Entities.First();
        }

        throw new Exception("Query returned more results than expected.");
    }

    public static async IAsyncEnumerable<Entity> RetrieveAllAsync(this IDataverseService service, QueryBase query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        switch (query)
        {
            case FetchExpression fe:
                var pageNumber = 1;
                var pageCookie = string.Empty;

                var fetchXml = XElement.Parse(fe.Query);
                fetchXml.SetAttributeValue("page", pageNumber);
                fetchXml.SetAttributeValue("paging-cookie", pageCookie);
                fe.Query = fetchXml.ToString();

                while (true)
                {
                    var results = await service.RetrieveMultipleAsync(fe, cancellationToken);

                    foreach (var entity in results.Entities)
                    {
                        yield return entity;
                    }

                    if (!results.MoreRecords)
                    {
                        break;
                    }

                    fetchXml.SetAttributeValue("page", pageNumber++);
                    fetchXml.SetAttributeValue("paging-cookie", results.PagingCookie);
                    fe.Query = fetchXml.ToString();
                }

                break;
            case QueryExpression qe:
                qe.PageInfo = new PagingInfo
                {
                    PageNumber = 1,
                };

                while (true)
                {
                    var results = await service.RetrieveMultipleAsync(qe, cancellationToken);

                    foreach (var entity in results.Entities)
                    {
                        yield return entity;
                    }

                    if (!results.MoreRecords)
                    {
                        break;
                    }

                    qe.PageInfo.PageNumber++;
                    qe.PageInfo.PagingCookie = results.PagingCookie;
                }
                break;
            case QueryByAttribute qba:
                qba.PageInfo = new PagingInfo
                {
                    PageNumber = 1,
                };

                while (true)
                {
                    var results = await service.RetrieveMultipleAsync(qba, cancellationToken);

                    foreach (var entity in results.Entities)
                    {
                        yield return entity;
                    }

                    if (!results.MoreRecords)
                    {
                        break;
                    }

                    qba.PageInfo.PageNumber++;
                    qba.PageInfo.PagingCookie = results.PagingCookie;
                }
                break;
            default:
                throw new NotSupportedException("Unknown query object.");
        }
    }
}
