using AutoMapper;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Modules.UsersModule.Models;

namespace HospitalManagement.Services.Modules.UsersModule.Maps
{
    internal class MapUpdateUser : Profile
    {
        public MapUpdateUser()
        {
            CreateMap<UpdateUser, User>()
                .ConvertUsing<MapUpdateUserConverter>()
                ;
        }
    }

    internal class MapUpdateUserConverter : ITypeConverter<UpdateUser, User>
    {
        public User Convert(UpdateUser source, User destination, ResolutionContext context)
        {
            return new User
            {
                EmailAddress = source.EmailAddress,
                Password = source.Password
            };
        }
    }
}
