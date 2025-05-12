namespace apbdtest1.Models.DTOs;

public class AppointmentDTO
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string Pwz { get; set; }
    public List<AppointmentServiceO> AppointmentServices { get; set; }
}