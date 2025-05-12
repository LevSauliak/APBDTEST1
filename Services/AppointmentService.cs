using apbdtest1.Exceptions;
using apbdtest1.Models;
using apbdtest1.Repositories;

namespace apbdtest1.Services;

public class AppointmentService: IAppointmentService
{
    IDbRepository _repository;

    public AppointmentService(IDbRepository repository)
    {
        this._repository = repository;
    }
    public async Task<AppointmentInfo> GetAppointmentInfo(int id)
    {
        AppointmentInfo result = new AppointmentInfo();
        
        Appointment? appointment = await _repository.GetAppointment(id);
        if (appointment == null) throw new NotFoundException("No appointment found");
        
        Patient? patient = await _repository.GetPatient(appointment.Patient_Id);
        if (patient == null) throw new NotFoundException("No patient found");
        
        Doctor? doctor = await _repository.GetDoctor(appointment.Doctor_Id);
        if (doctor == null) throw new NotFoundException("No doctor found");
        
        result.Date = appointment.Date;
        result.Patient = patient;
        result.DoctorInfo = new DoctorInfo()
        {
            DoctorId = doctor.Id,
            Pwz = doctor.Pwz,
        };
        
        var services = await _repository.GetAppointmentServices(appointment.Id);
        result.AppointmentServices = services;
        
        return result;
    }
}