using HMS_API.Models;
using HMS_API.Models.Dto;
using HMS_API.Models.Dto.PostDtos;

namespace HMS_API.Repository.IRepository
{
    public interface IDoctorRepository
    {
        Task<List<DoctorViewDto>> GetAllDoctor();
        Task<DoctorViewDto> GetDoctorById(string id);
        Task<DoctorViewDto> AddDepartmentToDoctor(AddDepartmentToDoctorDto departments);
    }
}
