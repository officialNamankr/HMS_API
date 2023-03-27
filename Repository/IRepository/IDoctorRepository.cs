using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;
using Microsoft.CodeAnalysis.Differencing;

namespace HMS_API.Repository.IRepository
{
    public interface IDoctorRepository
    {
        Task<List<DoctorViewDto>> GetAllDoctor();
        Task<DoctorViewDto> EditDoctor(string id, EditDoctorDto doc);
        Task<DoctorViewDto> GetDoctorById(string id);
        Task<DoctorViewDto> AddDepartmentToDoctor(AddDepartmentToDoctorDto departments);
    }
}
