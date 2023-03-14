using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    
    public class AppointmemtController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IAppointmentRepository _Appointmentrepository;
        public AppointmemtController(ApplicationDbContext db, IAppointmentRepository appointmentrepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _Appointmentrepository = appointmentrepository;
        }
        [HttpGet]
        [Route("/GetAllAppointments")]
        public async Task<ResponseDto> GetAllAppointment()
        {
            try
            {
                var result = await _Appointmentrepository.GetAllAppointments();
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("/GetAppointmentById")]
        public async Task<ResponseDto> GetAppointmentById(Guid id)
        {
            try
            {
                var result = await _Appointmentrepository.GetAppointmentById(id);
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("/GetAppointmentByPatientId")]
        public async Task<ResponseDto> GetAppointmentByPatientId(string id)
        {
            try
            {
                var result = await _Appointmentrepository.GetAppointmentByPatient(id);
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("/GetAppointmentByDoctorId")]
        public async Task<ResponseDto> GetAppointmentByDoctorId(string id)
        {
            try
            {
                var result = await _Appointmentrepository.GetAppointmentByDoctor(id);
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        public async Task<ResponseDto> PostAppointment([FromBody] AddAppointmentApi addappointmentApi)
        {
            try
            {
                DateOnly dateOnly = DateOnly.FromDateTime(addappointmentApi.Time_ofAppointment);
                TimeOnly timeOnly = TimeOnly.FromDateTime(addappointmentApi.Time_ofAppointment);
                var addappointment = new AddAppointment
                {
                    Date_Of_Appointment = dateOnly,
                    Time_Of_Appointment = timeOnly,
                    PatientId = addappointmentApi.PatientId,
                    DoctorId = addappointmentApi.DoctorId
                };
                var result = await _Appointmentrepository.AddAppointment(addappointment);
                _response.Result = Ok(addappointment);
                _response.DisplayMessage = "Appointment Added Successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

            
    }
}
