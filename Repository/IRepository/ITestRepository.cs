using HMS_API.Models;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;

namespace HMS_API.Repository.IRepository
{
    public interface ITestRepository
    {
        Task<object> GetAllTest();
        Task<bool> DeleteTest(Guid id);
        Task<object> GetTestById(Guid id);
        Task<object> EditTest(Guid id,EditTestDto model);

        Task<Test> AddTest(AddTestDto test);
    }
}
