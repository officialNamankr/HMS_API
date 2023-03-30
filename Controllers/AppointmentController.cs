using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IAppointmentRepository _Appointmentrepository;
        public AppointmentController(ApplicationDbContext db, IAppointmentRepository appointmentrepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _Appointmentrepository = appointmentrepository;
        }
        [HttpGet]
        [Route("/GetAllAppointments")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Doctor")]

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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Doctor")]
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Doctor,Patient")]
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
        [Authorize(AuthenticationSchemes="Bearer", Roles ="Admin,Doctor")]
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
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



        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<ResponseDto> DeleteAppointment(Guid appointmentId)
        {
            try
            {
                var result = await _Appointmentrepository.CancelAppointment(appointmentId);
                _response.Result = NoContent();
                _response.DisplayMessage = "Appointment cancelled Successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }



        [HttpGet]
        [Route("/GetCancelledAppointmentByPatient")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<ResponseDto> GetCancelledAppointmentByPatient()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetCancelledAppointmentByPatient(userId);
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
        [Route("/GetPastAppointmentByPatient")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<ResponseDto> GetPastAppointmentByPatient()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetPastAppointmentByPatient(userId);
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
        [Route("/GetUpcomingAppointmentByPatient")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<ResponseDto> GetUpcomingAppointmentByPatient()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetUpcomingAppointmentByPatient(userId);
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
        [Route("/GetTimeByDateAndDoctorId")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<ResponseDto> GetTimeByDateAndDoctorId(string data)
        {
            try
            {
                string[] dataValue = data.Split('/');
                
                DateTime datetime = DateTime.Parse(dataValue[1]);
                DateOnly date = DateOnly.FromDateTime(datetime);
                //var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetTimeByDateAndDoctorId(dataValue[0] ,date);
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
        [Route("/GetCancelledAppointmentByDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<ResponseDto> GetCancelledAppointmentByDoctor()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetCancelledAppointmentByDoctor(userId);
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
        [Route("/GetPastAppointmentByDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<ResponseDto> GetPastAppointmentByDoctor()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetPastAppointmentByDoctor(userId);
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
        [Route("/GetUpcomingAppointmentByDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<ResponseDto> GetUpcomingAppointmentByDoctor()
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var result = await _Appointmentrepository.GetUpcomingAppointmentByDoctor(userId);
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
        [Route("/GetCancelledAppointmentByPatientId")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ResponseDto> GetCancelledAppointmentByPatientId(string id)
        {
            try
            {
                
                var result = await _Appointmentrepository.GetCancelledAppointmentByPatient(id);
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
        [Route("/GetPastAppointmentByPatientId")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ResponseDto> GetPastAppointmentByPatientId(string id)
        {
            try
            {
                
                var result = await _Appointmentrepository.GetPastAppointmentByPatient(id);
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
        [Route("/GetUpcomingAppointmentByPatientId")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ResponseDto> GetUpcomingAppointmentByPatientId(string id)
        {
            try
            {
               
                var result = await _Appointmentrepository.GetUpcomingAppointmentByPatient(id);
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }




    }
}
