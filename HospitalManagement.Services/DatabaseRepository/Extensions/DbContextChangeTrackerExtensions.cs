using System.Linq.Expressions;
using HospitalManagement.Services.DatabaseRepository.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HospitalManagement.Services.DatabaseRepository.Extensions;

public static class DbContextChangeTrackerExtensions
{
    private static IMutableProperty GetOrAddProperty(this IMutableEntityType mutableEntityType, string name,
        Type propertyType)
    {
        return mutableEntityType?.FindProperty(name) ?? mutableEntityType.AddProperty(name, propertyType);
    }

    public static ModelBuilder AddChangeTracker(this ModelBuilder builder, Type typeOfUserId)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var shouldIgnoreChangeTracker = entityType.ClrType.CustomAttribute<IgnoreChangeTrackerAttribute>();

            if (shouldIgnoreChangeTracker != null) continue;

            var changeTracker = entityType.ClrType.CustomAttribute<UseChangeTrackerAttribute>();
            if (changeTracker != null)
            {
                if (changeTracker.ShouldUseCreatedOn)
                {
                    entityType.GetOrAddProperty(changeTracker.NameOfCreatedByProperty, typeOfUserId.ToNullableType());
                    entityType.GetOrAddProperty(changeTracker.NameOfCreatedOnProperty, typeof(DateTime?));
                }

                if (changeTracker.ShouldUseModifiedOn)
                {
                    entityType.GetOrAddProperty(changeTracker.NameOfModifiedByProperty, typeOfUserId.ToNullableType());
                    entityType.GetOrAddProperty(changeTracker.NameOfModifiedOnProperty, typeof(DateTime?));
                }
            }

            var softDelete = entityType.ClrType.CustomAttribute<UseSoftDeleteAttribute>();

            if (softDelete != null)
            {
                entityType.GetOrAddProperty(softDelete.NameOfDeletedByProperty, typeOfUserId.ToNullableType());

                var deletedOnProperty =
                    entityType.GetOrAddProperty(softDelete.NameOfDeletedOnProperty, typeof(DateTime?));

                var primaryKeyProperty = entityType.FindPrimaryKey().Properties.Single();
                var primaryKey = entityType.GetOrAddProperty(primaryKeyProperty.Name,
                    primaryKeyProperty.PropertyInfo.PropertyType);

                entityType.AddIndex(new[] { primaryKey, deletedOnProperty });

                var parameter = Expression.Parameter(entityType.ClrType);
                var propertyMethodInfo = typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(DateTime?));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter,
                    Expression.Constant(softDelete.NameOfDeletedOnProperty));

                var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty,
                    Expression.Constant(null, typeof(DateTime?)));

                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }

            builder.Entity(entityType.ClrType);
        }

        return builder;
    }

    public static ChangeTracker UseChangeTracker<T>(this ChangeTracker tracker, T userId)
    {
        foreach (var entry in tracker.Entries())
        {
            var changeTracker = entry.Metadata.ClrType.CustomAttribute<UseChangeTrackerAttribute>();
            var softDelete = entry.Metadata.ClrType.CustomAttribute<UseSoftDeleteAttribute>();

            if (changeTracker == null && softDelete == null) continue;

            if (changeTracker.ShouldUseCreatedOn)
            {
                entry.CurrentValues[changeTracker.NameOfCreatedByProperty] =
                    entry.OriginalValues[changeTracker.NameOfCreatedByProperty] ?? default;

                entry.CurrentValues[changeTracker.NameOfCreatedOnProperty] =
                    entry.OriginalValues[changeTracker.NameOfCreatedOnProperty] ?? default;
            }

            if (changeTracker.ShouldUseModifiedOn)
            {
                entry.CurrentValues[changeTracker.NameOfModifiedByProperty] =
                    entry.OriginalValues[changeTracker.NameOfModifiedByProperty] ?? default;

                entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] =
                    entry.OriginalValues[changeTracker.NameOfModifiedOnProperty] ?? default;
            }

            //TODO: Come back to this, since the old method is getting deprecated.
            if (softDelete?.UseCascadeDeletion ?? false)
            {
                //entry.ApplyCascadeDelete();
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    if (changeTracker
                        .ShouldUseCreatedOn /*&& entry.CurrentValues[changeTracker.NameOfCreatedOnProperty] != null*/)
                    {
                        entry.CurrentValues[changeTracker.NameOfCreatedOnProperty] = DateTime.UtcNow;
                        /*if (userId != null)*/
                        entry.CurrentValues[changeTracker.NameOfCreatedByProperty] = userId ?? default;
                    }

                    if (changeTracker.ShouldUseModifiedOn /*&&
                        entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] != null*/)
                    {
                        entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] = DateTime.UtcNow;
                        /*if (userId != null)*/
                        entry.CurrentValues[changeTracker.NameOfModifiedByProperty] = userId ?? default;
                    }

                    break;

                case EntityState.Modified:
                    if (changeTracker
                        .ShouldUseModifiedOn /*&& entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] != null*/)
                    {
                        entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] = DateTime.UtcNow;
                        /*if (userId != null)*/
                        entry.CurrentValues[changeTracker.NameOfModifiedByProperty] = userId ?? default;
                    }

                    break;

                case EntityState.Deleted:
                    if (softDelete != null /* && entry.CurrentValues[softDelete.NameOfDeletedOnProperty] != null*/)
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[softDelete.NameOfDeletedOnProperty] = DateTime.UtcNow;
                        /*if (userId != null)*/
                        entry.CurrentValues[softDelete.NameOfDeletedByProperty] = userId ?? default;
                    }

                    if (changeTracker.ShouldUseModifiedOn &&
                        entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] != null)
                    {
                        entry.CurrentValues[changeTracker.NameOfModifiedOnProperty] = DateTime.UtcNow;
                        /*if (userId != null)*/
                        entry.CurrentValues[changeTracker.NameOfModifiedByProperty] = userId ?? default;
                    }

                    break;
            }
        }

        return tracker;
    }
}