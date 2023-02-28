﻿using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ApplicationDbContext db, 
            IDepartmentRepository departmentRepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _departmentRepository = departmentRepository;
        }


        [HttpPost]
        [Route("AddDepartment")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<object> AddSkill(AddDepartmentDto dept)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DepartmentViewDto model = await _departmentRepository.AddDepartment(dept);

                    _response.Result = Ok(model);
                    _response.DisplayMessage = "Department Added Successfully";
                    //return Ok(model);
                }
                else
                {
                    _response.Result = BadRequest(ModelState);
                    _response.DisplayMessage = "Error in adding skill";
                    //return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", ex.Message);
                //return BadRequest(ModelState);
                _response.Result = BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }


    }
}
