using apbdtest1.Models;

namespace apbdtest1.Repositories;

public interface IDbRepository
{
    Task<Patient?> GetPatient(int id);
    Task<Appointment?> GetAppointment(int id);
    Task<List<AppointmentServiceO>> GetAppointmentServices(int id);
    Task<Doctor?> GetDoctor(int id);
}