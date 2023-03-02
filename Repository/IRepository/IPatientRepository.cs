using HMS_API.Models.Dto;

namespace HMS_API.Repository.IRepository
{
    public interface IPatientRepository
    {
        Task<List<PatientViewDto>> GetAllPatient();
        Task<PatientViewDto> GetPatientById(string id);
        Task<PatientViewDto> GetPatientByUserName(string username);
    }
}
