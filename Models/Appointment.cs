namespace apbdtest1.Models;

public class Appointment
{
    public int Id { get; set; }
    public int Patient_Id { get; set; }
    public int Doctor_Id { get; set; }
    public DateTime Date { get; set; }
}