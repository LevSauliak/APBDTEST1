using apbdtest1.Models;

namespace apbdtest1.Services;

public interface IAppointmentService
{
    public Task<AppointmentInfo> GetAppointmentInfo(int appointmentId);
}