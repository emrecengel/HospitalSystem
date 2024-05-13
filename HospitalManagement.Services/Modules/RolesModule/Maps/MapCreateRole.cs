using AutoMapper;
using HospitalManagement.Services.Modules.RolesModule.Commands;
using HospitalManagement.Services.Modules.RolesModule.Models;

namespace HospitalManagement.Services.Modules.RolesModule.Maps;

internal class MapCreateRole : Profile
{
    public MapCreateRole()
    {
        CreateMap<CreateRole, Role>()
            .ConvertUsing<MapCreateRoleConverter>()
            ;
    }
}

internal class MapCreateRoleConverter : ITypeConverter<CreateRole, Role>
{
    public Role Convert(CreateRole source, Role destination, ResolutionContext context)
    {
        return new Role
        {
            Name = source.Name,
            Description = source.Description
        };
    }
}


