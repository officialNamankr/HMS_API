using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task<object> GetAllTest()
        {
            var tests = await _db.Tests.ToListAsync();
            return tests;
        }

        public async Task<object> GetTestById(Guid id)
        {
            var test = await _db.Tests.Where(t => t.TestId.Equals(id)).FirstOrDefaultAsync();
            return test;
        }
    }
}
