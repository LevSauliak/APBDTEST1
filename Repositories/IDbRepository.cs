using apbdtest1.Models;
using apbdtest1.Models.DTOs;

namespace apbdtest1.Repositories;

public interface IDbRepository
{
    Task<Patient?> GetPatient(int id);
    Task<Appointment?> GetAppointment(int id);
    Task<List<AppointmentServiceO>> GetAppointmentServices(int id);
    Task<Doctor?> GetDoctor(int id);
    Task<Doctor?> GetDoctorByPWZ(string pwz);
    
    Task<bool> AppointmentExists(int id);
    
    Task<bool> AddAppointment(AppointmentDTO appointment);
    
    Task<bool> ServiceExists(string name);
}