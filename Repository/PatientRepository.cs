using HMS_API.DbContexts;
using HMS_API.Models.Dto;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HMS_API.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<PatientViewDto>> GetAllPatient()
        {
            var patientIds = await _db.Patients.ToListAsync();
            List<PatientViewDto> patients = new List<PatientViewDto>();
            foreach (var patientId in patientIds)
            {
                var PatientDetails = await _db.Users.Where(u => u.Id.Equals(patientId.PatientId)).FirstOrDefaultAsync();
                if (PatientDetails == null)
                {
                    continue;
                }
                



                var patient = new PatientViewDto()
                {
                    PatientId = patientId.PatientId,
                    Name = PatientDetails.Name,
                    UserName = PatientDetails.UserName,
                    Email = PatientDetails.Email,
                    
                };
                patients.Add(patient);
            }

            return patients;

        }
        public async Task<PatientViewDto> GetPatientById(string id)
        {
            var PatientDetails = await _db.Users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
            if (PatientDetails == null)
            {
                return null;
            }
            
            
            var patient = new PatientViewDto()
            {
                PatientId = id,
                Name = PatientDetails.Name,
                UserName = PatientDetails.UserName,
                Email = PatientDetails.Email,
            };
            return patient;
        }
        public async Task<PatientViewDto> GetPatientByUserName(string username)
        {
            var PatientDetails = await _db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefaultAsync();
            if (PatientDetails == null)
            {
                return null;
            }
            

            var patient = new PatientViewDto()
            {
                PatientId = PatientDetails.Id,
                Name = PatientDetails.Name,
                UserName = PatientDetails.UserName,
                Email = PatientDetails.Email,
            };
            return patient;
        }
    }
}
