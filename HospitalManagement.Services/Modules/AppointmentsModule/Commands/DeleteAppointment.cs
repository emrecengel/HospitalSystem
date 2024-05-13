using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Commands;

public class DeleteAppointment : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteAppointment UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteAppointment(
    IRepository<Appointment> repository) : IRequestHandler<DeleteAppointment, Unit>
{
    public async Task<Unit> Handle(DeleteAppointment request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}