using Microsoft.AspNetCore.Authorization;
using HMS_API.DbContexts;

using HMS_API.Models;
using HMS_API.Models.Dto;
using HMS_API.Services.TokenGenerators;
using HMS_API.Services.TokenValidators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using HMS_API.Models.Response;
using HMS_API.Helper;
using HMS_API.Models.Dto.GetDtos;
//using HMS_API.Models.Response;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            IJWTManagerRepository jwtManagerRepository, RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator)
        {
            _db = db;
            this._response = new ResponseDto();
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            //_jwtManagerRepository = jwtManagerRepository;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }



        [HttpGet]
        [Route("createRoles")]
        public async Task<object> CreateRoles()
        {
            try
            {
                if (!_roleManager.RoleExistsAsync(Helper.Helper.Admin).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Admin));
                    await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Doctor));
                    await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Patient));
                }
                _response.Result = Ok();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("Register")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        //Register a user(Admin,Doctor or Patient)
        public async Task<object> Register([FromBody] RegisterViewDto model)
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
                            
                                var patient = new Patient
                                {
                                    PatientId = user.Id
                                };
                                await _db.Patients.AddAsync(patient);

                            
                            await _userManager.AddToRoleAsync(user, Helper.Helper.Patient);
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

        [HttpPost]
        [Route("RegisterAsAdmin")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        //Register a user(Admin,Doctor or Patient)
        public async Task<object> RegisterAsAdmin([FromBody] RegisterViewDto model)
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
                        
                            


                            await _userManager.AddToRoleAsync(user, Helper.Helper.Admin);
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

        [HttpPost]
        [Route("LoginJWT")]
        public async Task<object> Login2([FromBody] LoginViewDto model)
        {

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Result = BadRequest();
                _response.DisplayMessage = "Invalid model state";
                return _response;
            }
            else
            {
                ApplicationUser user = await _db.Users.Where(u => u.Email.Equals(model.Email)).FirstOrDefaultAsync();
                if (user == null)
                {
                    _response.IsSuccess = false;
                    _response.Result = Unauthorized();
                    _response.DisplayMessage = "User Does not Exist";
                    return _response;
                }
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isCorrectPassword)
                {
                    _response.IsSuccess = false;
                    _response.Result = Unauthorized();
                    _response.DisplayMessage = "Password Incorrect";
                    return _response;
                }

                var accessToken = _accessTokenGenerator.GenerateToken(user);
                string refreshToken = _refreshTokenGenerator.GenerateToken();

                RefreshToken newRefreshToken = new RefreshToken()
                {
                    Token = refreshToken,
                    UserId = user.Id,
                };
                await _refreshTokenRepository.Create(newRefreshToken);
                _response.Result = Ok(new AuthenticatedUserResponse()
                {
                    AccessToken = accessToken.Result,
                    RefreshToken = refreshToken,
                });

                _response.DisplayMessage = "Logged In successfully";
                return (_response);

            }

        }



        [HttpDelete]
        [Route("Logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> Logout()
        {
            var userId = User.FindFirstValue("id"); //gives the useriD of the logged in User
            if (userId == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "first login in to access";
                _response.Result = Unauthorized();
                return _response;
            }
            await _refreshTokenRepository.DeleteAll(userId);//deletes the refresh tokens of the user to logout

            _response.IsSuccess = true;
            _response.DisplayMessage = "Logged out Successfully";
            _response.Result = NoContent();

            return _response;
        }



    }
}
