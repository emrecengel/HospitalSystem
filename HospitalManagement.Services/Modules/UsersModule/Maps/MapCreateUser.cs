using AutoMapper;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Modules.UsersModule.Models;

namespace HospitalManagement.Services.Modules.UsersModule.Maps;

internal class MapCreateUser : Profile
{
    public MapCreateUser()
    {
        CreateMap<CreateUser, User>()
            .ConvertUsing<MapCreateUserConverter>()
            ;
    }
}

internal class MapCreateUserConverter : ITypeConverter<CreateUser, User>
{
    public User Convert(CreateUser source, User destination, ResolutionContext context)
    {
        return new User
        {
            EmailAddress = source.EmailAddress,
            Password = source.Password
        };
    }
}


