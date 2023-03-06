using HMS_API.Models;
using HMS_API.Models.Dto;

namespace HMS_API.Repository.IRepository
{
    public interface ITestRepository
    {
        Task<object> GetAllTest();
        Task<object> GetTestById(Guid id);

        Task<Test> AddTest(AddTestDto test);
    }
}
