using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IPatientRepository _Patientrepository;


        public PatientController(ApplicationDbContext db, IPatientRepository patientrepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _Patientrepository = patientrepository;

        }
        [HttpGet]
        [Route("/GetAllPatients")]
        public async Task<ResponseDto> GetPatient()
        {
            try
            {
                var result = await _Patientrepository.GetAllPatient();
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
        [Route("/GetPatientById")]
        public async Task<ResponseDto> GetPatientById(string id)
        {
            try
            {
                var result = await _Patientrepository.GetPatientById(id);
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
        [Route("/GetPatientByUserName")]
        public async Task<ResponseDto> GetPatientByUserName(string id)
        {
            try
            {
                var result = await _Patientrepository.GetPatientByUserName(id);
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
