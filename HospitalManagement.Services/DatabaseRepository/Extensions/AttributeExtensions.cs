using System.Reflection;

namespace HospitalManagement.Services.DatabaseRepository.Extensions;

internal static class AttributeExtensions
{
    public static TAttribute CustomAttribute<TAttribute>(this Type type) where TAttribute : Attribute
    {
        return (TAttribute)Attribute.GetCustomAttribute(type, typeof(TAttribute));
    }

    public static TAttribute PropertyCustomAttribute<TAttribute>(this PropertyInfo entryType)
        where TAttribute : Attribute
    {
        return (TAttribute)Attribute.GetCustomAttribute(entryType, typeof(TAttribute));
    }
}