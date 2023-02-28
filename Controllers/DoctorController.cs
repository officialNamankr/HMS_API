using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models;
using HMS_API.Repository.IRepository;
using HMS_API.Services.TokenGenerators;
using HMS_API.Services.TokenValidators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HMS_API.Models.Dto.PostDtos;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IDoctorRepository _doctorrepository;
        

        public DoctorController(ApplicationDbContext db, IDoctorRepository doctorrepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _doctorrepository = doctorrepository;
       
        }


        [HttpGet]
        [Route("/GetAllDoctors")]
        public async Task<ResponseDto> GetTrainers()
        {
            try
            {
                var result = await _doctorrepository.GetAllDoctor();
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
        [Route("/GetDoctorById")]
        public async Task<ResponseDto> GetDoctorById(string id)
        {
            try
            {
                var result = await _doctorrepository.GetDoctorById(id);
                _response.Result = Ok(result);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPut("/AddDeptToDoc")]
        public async Task<ActionResult<object>> AddDeptToDoc([FromBody]AddDepartmentToDoctorDto model)
        {
            try
            {

                var result = await _doctorrepository.AddDepartmentToDoctor(model);

                if (result != null)
                {
                    _response.Result = Ok(result);
                }
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Error updating data");
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
