namespace HospitalManagement.Services.DatabaseRepository.Attributes;

public sealed class UseSoftDeleteAttribute : Attribute
{
    public string NameOfDeletedOnProperty { get; set; } = "DeletedOn";
    public string NameOfDeletedByProperty { get; set; } = "DeletedBy";
    public bool UseCascadeDeletion { get; set; } = false;
}