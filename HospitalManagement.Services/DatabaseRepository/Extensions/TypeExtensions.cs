namespace HospitalManagement.Services.DatabaseRepository.Extensions;

internal static class TypeExtensions
{
    public static Type ToNullableType(this Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
        return type.IsValueType ? typeof(Nullable<>).MakeGenericType(type) : type;
    }
}