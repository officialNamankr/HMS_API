using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace HMS_API.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _db;
        public TestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Test> AddTest(AddTestDto test)
        {
            var testDetial = new Test
            {
                Name = test.Name,
                Description = test.Description,
            };
            await _db.Tests.AddAsync(testDetial);
            await _db.SaveChangesAsync();
            return testDetial;

        }

        public async Task<bool> DeleteTest(Guid id)
        {
            var test = await _db.Tests.FindAsync(id);
            if (test == null)
            {
                return true;
            }
            test.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<object> EditTest(Guid id, EditTestDto model)
        {
            var test = await _db.Tests.FindAsync(id);
            if(test == null)
            {
                return null;
            }
            test.Description = model.Description;
            test.Name = model.Name;
            _db.SaveChanges();
            return test;
        }

        public async Task<object> GetAllTest()
        {
            var tests = await _db.Tests.OrderBy(t => t.Name).ToListAsync();
            return tests;
        }

        public async Task<object> GetTestById(Guid id)
        {
            var test = await _db.Tests.Where(t => t.TestId.Equals(id)).FirstOrDefaultAsync();
            return test;
        }
    }
}



