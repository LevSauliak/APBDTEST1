using apbdtest1.Models;
using apbdtest1.Models.DTOs;

namespace apbdtest1.Services;

public interface IAppointmentService
{
    public Task<AppointmentInfo> GetAppointmentInfo(int appointmentId);
    
    public Task<bool> AddAppointment(AppointmentDTO appointmentDto);
}