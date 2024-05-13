namespace HospitalManagement.Services.DatabaseRepository.Attributes;

public sealed class UseChangeTrackerAttribute : Attribute
{
    public bool ShouldUseCreatedOn { get; set; } = true;
    public bool ShouldUseModifiedOn { get; set; } = true;
    public string NameOfCreatedOnProperty { get; set; } = "CreatedOn";
    public string NameOfCreatedByProperty { get; set; } = "CreatedBy";
    public string NameOfModifiedOnProperty { get; set; } = "ModifiedOn";
    public string NameOfModifiedByProperty { get; set; } = "ModifiedBy";
}