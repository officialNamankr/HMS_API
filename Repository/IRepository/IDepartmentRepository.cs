using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;

namespace HMS_API.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        Task<DepartmentViewDto> AddDepartment(AddDepartmentDto department);
        Task<List<DepartmentViewDto>> GetAllDepartments();
    }
}
