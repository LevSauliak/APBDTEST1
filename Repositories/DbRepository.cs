using apbdtest1.Models;
using apbdtest1.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace apbdtest1.Repositories;

public class DbRepository: IDbRepository
{
    private readonly IConfiguration _configuration;
    public DbRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Patient?> GetPatient(int id)
    {
        string sql = "select first_name, last_name, date_of_birth from patient where patient_id = @Id";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@Id", id);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        
        await reader.ReadAsync();

        if (!reader.HasRows)
        {
            return null;
        }

        var Patient = new Patient()
        {
            FirstName = reader["first_name"].ToString(),
            LastName = reader["last_name"].ToString(),
            DateOfBirth = (DateTime)reader["date_of_birth"],
        };
        
        return Patient;
    }

    public async Task<Appointment?> GetAppointment(int id)
    {
        string sql = "select TOP 1 appointment_id, patient_id, doctor_id, date from Appointment where appointment_id = @Id";

        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@Id", id);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        
        await reader.ReadAsync();

        if (!reader.HasRows)
        {
            return null;
        }
        var appointment = new Appointment()
        {
            Id = (int)reader["appointment_id"],
            Patient_Id = (int)reader["patient_id"],
            Doctor_Id = (int)reader["doctor_id"],
            Date = (DateTime)reader["date"],
        };
        
        return appointment;
    }

    public async Task<Doctor> GetDoctor(int id)
    {
        string sql = "select doctor_id, first_name, last_name, PWZ from doctor where doctor_id = @Id";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@Id", id);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        
        await reader.ReadAsync();

        if (!reader.HasRows)
        {
            return null;
        }

        var doctor = new Doctor()
        {
            Id = (int)reader["doctor_id"],
            First_Name = (string)reader["first_name"],
            Last_Name = (string)reader["last_name"],
            Pwz = (string)reader["PWZ"],
        };
        return doctor;
    }
    
    public async Task<List<AppointmentServiceO>> GetAppointmentServices(int id)
    {
        
        List<AppointmentServiceO> appointmentServices = new List<AppointmentServiceO>();
        
        string sql = @"select s.name as name, aps.service_fee as fee
                        from Appointment a 
                        join appointment_service aps on a.appointment_id = aps.appointment_id
                        join service s on s.service_id = aps.service_id
                        where a.appointment_id = @Id
                        ";
        
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@Id", id);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            appointmentServices.Add(new AppointmentServiceO()
            {
                Name = (string)reader["name"],
                ServiceFee = Convert.ToDouble(reader["fee"]),
            });
        }

        return appointmentServices;
    }

    public async Task<Doctor?> GetDoctorByPWZ(string pwz)
    {
        
        string sql = "select doctor_id, first_name, last_name, PWZ from doctor where PWZ = @PWZ";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@PWZ", pwz);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        
        await reader.ReadAsync();

        if (!reader.HasRows)
        {
            return null;
        }

        var doctor = new Doctor()
        {
            Id = (int)reader["doctor_id"],
            First_Name = (string)reader["first_name"],
            Last_Name = (string)reader["last_name"],
            Pwz = (string)reader["PWZ"],
        };
        return doctor;
    }

    public async Task<bool> AppointmentExists(int id)
    {
        string sql = "select appointment_id from Appointment where appointment_id = @Id";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@Id", id);
        
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        if (!reader.HasRows)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> AddAppointment(AppointmentDTO appointment)
    {
        // string apserv_sql = "insert into Appointment_Service(service_id, service_fee) VALUES (@id, @service_fee)";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        // await using var cmd = new SqlCommand(apserv_sql, con);

        return true;
    }

    public async Task<bool> ServiceExists(string name)
    {
        string sql = "select service_id from service where name = @Name";
        
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var cmd = new SqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@Name", name);
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        if (!reader.HasRows)
        {
            return false;
        }
        return true;
        
    }
}