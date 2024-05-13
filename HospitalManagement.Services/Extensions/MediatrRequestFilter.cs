using System.Linq.Dynamic.Core;
using HospitalManagement.Services.RequestTypes;

namespace HospitalManagement.Services.Extensions;

internal static class MediatrRequestFilter
{
    public static IQueryable<T> ApplyStringFilters<T>(this IQueryable<T> query, object request) where T : class
    {
        switch (request)
        {
            case IFilterableRequest filterableRequest:
            {
                if (!string.IsNullOrWhiteSpace(filterableRequest.Filter)) query = query.Where(filterableRequest.Filter);

                if (!string.IsNullOrWhiteSpace(filterableRequest.OrderBy))
                    query = query.OrderBy(!string.IsNullOrWhiteSpace(filterableRequest.OrderBy)
                        ? $"{filterableRequest.OrderBy} {filterableRequest.SortBy.ToString().ToLower()}"
                        : $"x=> x {filterableRequest.SortBy.ToString().ToLower()}");


                if (request is IPageableRequest { PageSize: not null } pageableRequest)
                    query = query.Skip(pageableRequest.PageIndex * pageableRequest.PageSize.Value)
                        .Take(pageableRequest.PageSize.Value);

                break;
            }
            case ICountableRequest countableRequest when !string.IsNullOrWhiteSpace(countableRequest.Filter):
                query = query.Where(countableRequest.Filter);
                break;
        }


        return query;
    }
}