namespace HospitalManagement.Services.RequestTypes;

public interface ICountableRequest : IFilteredRequest
{
}

public interface ISortedRequest
{
    SortBy? SortBy { get; set; }
}

public interface IOrderedRequest
{
    string? OrderBy { get; set; }
}

public interface IFilteredRequest
{
    string? Filter { get; set; }
}

public interface IFilterableRequest : ISortedRequest, IOrderedRequest, IFilteredRequest
{
}

public enum SortBy
{
    Ascending = 0,
    Descending = 1
}