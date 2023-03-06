using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto;
using HMS_API.Repository.IRepository;

namespace HMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly ITestRepository _testRepository;

        public TestsController(ApplicationDbContext db, ITestRepository testRepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _testRepository = testRepository;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ResponseDto> GetTests()
        {
            try
            {
                var tests = await _testRepository.GetAllTest();
                if(tests == null)
                {
                    _response.DisplayMessage = "No Test Found";
                    _response.Result = NoContent();
                    return _response;
                }
                _response.DisplayMessage = "Successfully fetched all the tests";
                _response.Result = Ok(tests);
                return _response;

            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message};
                return _response;
            }
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetTest(Guid id)
        {
            try
            {
                var test = await _testRepository.GetTestById(id);
                if(test == null)
                {
                    _response.Result = NoContent();
                    _response.DisplayMessage = "No Test found with the given Id";
                }
                else
                {
                    _response.Result = Ok(test);
                    _response.DisplayMessage = "Sucessfully fetched the Test";
                }
            }
            catch(Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
            }
            return _response;
        }

        

        // POST: api/Tests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseDto> PostTest(AddTestDto test)
        {
            try
            {
                var result = await  _testRepository.AddTest(test);
                _response.Result = Ok(test);
                _response.DisplayMessage = "Test Added Successfully";
                return _response;

            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;   
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var test = await _db.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }

            _db.Tests.Remove(test);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool TestExists(Guid id)
        {
            return _db.Tests.Any(e => e.TestId == id);
        }
    }
}
