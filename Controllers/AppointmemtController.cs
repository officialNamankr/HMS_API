using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS_API.Controllers
{
    
=======

namespace HMS_API.Controllers
{
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
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
        public async Task<ResponseDto> GetPatient()
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
        public async Task<ResponseDto> GetAppointmentById(string id)
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
<<<<<<< HEAD
        public async Task<ResponseDto> PostTest([FromBody] AddAppointmentApi addappointmentApi)
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
=======
        public async Task<ResponseDto> PostTest(AddAppointment addappointment)
        {
            try
            {
                var result = await _Appointmentrepository.AddAppointment(addappointment);
                _response.Result = Ok(addappointment);
                _response.DisplayMessage = "Test Added Successfully";
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
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
