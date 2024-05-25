using AutoMapper;
using HospitalManagement.Services.Modules.RolesModule.Commands;
using HospitalManagement.Services.Modules.RolesModule.Models;

namespace HospitalManagement.Services.Modules.RolesModule.Maps;

internal class MapUpdateRole : Profile
{
    public MapUpdateRole()
    {
        CreateMap<UpdateRole, Role>()
            .ConvertUsing<MapUpdateRoleConverter>()
            ;
    }
}

internal class MapUpdateRoleConverter : ITypeConverter<UpdateRole, Role>
{
    public Role Convert(UpdateRole source, Role destination, ResolutionContext context)
    {
        destination ??= new Role();
        destination.Name = source.Name;
        destination.Description = source.Description;

        return destination;
    }
}