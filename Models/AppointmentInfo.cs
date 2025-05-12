namespace apbdtest1.Models;

public class AppointmentInfo
{
    public DateTime Date { get; set; }
    public Patient Patient { get; set; }
    public DoctorInfo DoctorInfo { get; set; }
    public List<AppointmentServiceO> AppointmentServices { get; set; }
}