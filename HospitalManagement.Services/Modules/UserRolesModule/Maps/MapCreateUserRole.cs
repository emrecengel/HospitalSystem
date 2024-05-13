using AutoMapper;
using HospitalManagement.Services.Modules.UserRolesModule.Commands;
using HospitalManagement.Services.Modules.UserRolesModule.Models;

namespace HospitalManagement.Services.Modules.UserRolesModule.Maps;

internal class MapCreateUserRole : Profile
{
    public MapCreateUserRole()
    {
        CreateMap<CreateUserRole, UserRole>()
            .ConvertUsing<MapCreateUserRoleConverter>()
            ;
    }
}

internal class MapCreateUserRoleConverter : ITypeConverter<CreateUserRole, UserRole>
{
    public UserRole Convert(CreateUserRole source, UserRole destination, ResolutionContext context)
    {
        return new UserRole
        {
            UserId = source.UserId,
            RoleId = source.RoleId
        };
    }
}