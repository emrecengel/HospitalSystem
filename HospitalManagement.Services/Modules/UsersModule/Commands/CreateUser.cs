using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UsersModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.UsersModule.Commands;

public class CreateUser : IRequest<User>
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}

internal sealed class HandleCreateUser(
    IRepository<User> repository,
    IMapper mapper) : IRequestHandler<CreateUser, User>
{
    public Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<User>(request));
    }
}