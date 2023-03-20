using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalReportController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IMedicalReportRepository _medicalReportRepository;

        public MedicalReportController(ApplicationDbContext db, IMedicalReportRepository medicalRepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _medicalReportRepository = medicalRepository;
        }


        [HttpGet]
        [Route("/GetMedicalReportByAppointment")]
        public async Task<ResponseDto> GetMedicalReportByAppointment(Guid id)
        {
            try
            {
                var result = await _medicalReportRepository.GetReportByAppointmentId(id);
                if (result == null)
                {
                    _response.DisplayMessage = "No Report found";
                    _response.Result = NoContent();
                    _response.IsSuccess = true;
                    return _response;
                }
                _response.Result = Ok(result);
                _response.IsSuccess = true;
                _response.DisplayMessage = "Medical Report Successfully Fetched";

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }






        [HttpPost]
        [Route("AddGeport")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
  
        public async Task<object> AddReport([FromBody] AddMedicalReport report)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var r = _medicalReportRepository.AddMedicalReport(report);
                    _response.DisplayMessage = "Report Added Successfully";
                    _response.Result = r;
                    _response.IsSuccess = true;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    //_response.ErrorMessages = new List<string>() { ex.ToString() };
                }
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
