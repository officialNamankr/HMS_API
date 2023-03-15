using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models;
using HMS_API.Repository.IRepository;
using HMS_API.Services.TokenGenerators;
using HMS_API.Services.TokenValidators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.GetDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IDoctorRepository _doctorrepository;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public DoctorController(ApplicationDbContext db, IDoctorRepository doctorrepository,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this._response = new ResponseDto();
            _doctorrepository = doctorrepository;
            _userManager = userManager;
       
        }


        [HttpGet]
        [Route("/GetAllDoctors")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Patient")]
        public async Task<ResponseDto> GetDoctors()
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
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



        [HttpPost]
        [Route("RegisterDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        //Register a user(Admin,Doctor or Patient)
        public async Task<object> Register([FromBody] RegisterDoctorDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Name = model.Name,
                        Addedon = DateTime.Now,

                    };
                    var IsUserNamePresent = await _db.Users
                        .AnyAsync(u => u.UserName == user.UserName);
                    var IsEmailPresent = await _db.Users.AnyAsync(u => u.Email == user.Email);
                    if (IsUserNamePresent)
                    {
                        _response.Result = BadRequest();
                        _response.DisplayMessage = "UserName Already Present";
                    }
                    else if (IsEmailPresent)
                    {
                        _response.Result = BadRequest();
                        _response.DisplayMessage = "Email Already Present";
                    }
                    else
                    {
                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            
                                var doctor = new Doctor
                                {
                                    DoctorId = user.Id
                                };
                                await _db.Doctors.AddAsync(doctor);

                           
                            await _userManager.AddToRoleAsync(user,Helper.Helper.Doctor);
                            await _db.SaveChangesAsync();
                            _response.Result = Ok();
                        }
                    }

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
