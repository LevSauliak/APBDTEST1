using apbdtest1.Exceptions;
using apbdtest1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbdtest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            this._appointmentService = appointmentService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentInfo(int id)
        {
            try
            {
                var appointment_info = await _appointmentService.GetAppointmentInfo(id);
                return Ok(appointment_info);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
