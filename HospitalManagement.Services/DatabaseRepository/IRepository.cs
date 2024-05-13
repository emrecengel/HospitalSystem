using EFCore.BulkExtensions;

namespace HospitalManagement.Services.DatabaseRepository;

public interface IRepository<T>
{
    IQueryable<T> Query { get; }
    IQueryable<T> FromRawSqlQuery(string sqlText, params object[] parameters);
    Task<T> Create(T value);
    Task Update(T value);
    Task Delete(T value);
    Task BulkCreate(List<T> values);
    Task BulkUpdate(List<T> values);
    Task BulkDelete(List<T> values);

    Task BulkUpsert(List<T> values, BulkConfig config);
    Task BulkRead(List<T> values, BulkConfig config);
}