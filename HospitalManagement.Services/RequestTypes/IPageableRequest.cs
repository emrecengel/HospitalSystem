namespace HospitalManagement.Services.RequestTypes;

public interface IPageableRequest
{
    int? PageSize { get; set; }
    int PageIndex { get; set; }
}