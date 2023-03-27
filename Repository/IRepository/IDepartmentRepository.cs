using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;

namespace HMS_API.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        Task<DepartmentViewDto> AddDepartment(AddDepartmentDto department);
        Task<List<DepartmentViewDto>> GetAllDepartments();
        Task<List<DoctorByDepartmentViewDTO>> GetAllDoctorsByDeptId(Guid id);
        Task<object> EditDepartment(Guid id, EditDepartmentDto model);
        Task<object> GetDepartmentById(Guid id);
        Task<bool> DeleteDepartment(Guid id);
    }
}
