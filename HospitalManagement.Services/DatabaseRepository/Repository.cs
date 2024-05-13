using System.Security.Principal;
using EFCore.BulkExtensions;
using HospitalManagement.Services.DatabaseRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalManagement.Services.DatabaseRepository;

internal sealed class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbContext _context;
    private readonly ILogger<Repository<T>> _logger;
    private readonly IPrincipal _principal;

    public Repository(DbContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IQueryable<T> Query => _context.Set<T>().AsNoTracking().AsQueryable();

    public async Task<T> Create(T value)
    {
        var entry = _context.Entry(value);
        entry.State = EntityState.Added;
        await SaveChangesAsync();
        entry.State = EntityState.Detached;
        _logger.LogInformation("Entity created: {@Entity}", value);
        return value;
    }


    public IQueryable<T> FromRawSqlQuery(string sqlText, params object[] parameters)
    {
        return _context.Set<T>().FromSqlRaw($"{sqlText}", parameters).AsNoTracking().AsQueryable();
    }

    public async Task Update(T value)
    {
        var entry = _context.Entry(value);
        entry.State = EntityState.Modified;

        var currentValues = await entry.GetDatabaseValuesAsync();

        if (currentValues != default) entry.OriginalValues.SetValues(currentValues);


        foreach (var property in entry.Properties)
        {
            if (property?.OriginalValue == null && property?.CurrentValue == null) property.IsModified = true;

            if (property is { IsModified: true, CurrentValue: null, OriginalValue: not null })
                property.IsModified = true;

            if (property is { IsModified: true, CurrentValue: null, OriginalValue: null })
                property.IsModified = false;

            if (property.IsModified && property?.OriginalValue == property?.CurrentValue) property.IsModified = false;

            if (property.IsModified && property is { OriginalValue: not null, CurrentValue: not null } &&
                property.OriginalValue.Equals(property.CurrentValue))
                property.IsModified = false;

            if (property.Metadata.PropertyInfo == null)
            {
            }
        }

        await SaveChangesAsync();
        entry.State = EntityState.Detached;
        _logger.LogInformation("Entity updated: {@Entity}", value);
    }

    public async Task Delete(T value)
    {
        var entry = _context.Entry(value);
        entry.State = EntityState.Deleted;
        await SaveChangesAsync();
        entry.State = EntityState.Detached;
        _logger.LogInformation("Entity deleted: {@Entity}", value);
    }

    public async Task BulkCreate(List<T> values)
    {
        await _context.BulkInsertAsync(values, config => { config.BatchSize = 5000; });
        _logger.LogInformation("Bulk created: {Count} entities", values.Count);
    }

    public async Task BulkUpdate(List<T> values)
    {
        await _context.BulkUpdateAsync(values, config => { config.BatchSize = 5000; });
        _logger.LogInformation("Bulk updated: {Count} entities", values.Count);
    }

    public Task BulkUpsert(List<T> values, BulkConfig config)
    {
        return _context.BulkInsertOrUpdateAsync(values, config);
    }

    public Task BulkRead(List<T> values, BulkConfig config)
    {
        return _context.BulkReadAsync(values, config);
    }

    public async Task BulkDelete(List<T> values)
    {
        await _context.BulkDeleteAsync(values, config => { config.BatchSize = 5000; });
        _logger.LogInformation("Bulk deleted: {Count} entities", values.Count);
    }

    public Task SaveChangesAsync()
    {
        _context.ChangeTracker.UseChangeTracker(_principal?.UserId());
        return _context.SaveChangesAsync();
    }
}