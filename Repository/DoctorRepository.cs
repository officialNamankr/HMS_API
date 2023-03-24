using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS_API.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _db;
        public DoctorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DoctorViewDto> AddDepartmentToDoctor(AddDepartmentToDoctorDto model)
        {
            var doctor = await _db.Doctors.Include(d => d.Departments).FirstOrDefaultAsync(doc => doc.DoctorId.Equals(model.DoctorId));
            var docGenDetails = await _db.Users.Where(u => u.Id.Equals(model.DoctorId)).FirstOrDefaultAsync();
            if (doctor == null)
            {
                return null;
            }
            
            
            foreach (var dept in model.DepartmentIds)
            {
                var dpt = await _db.Departments.FirstOrDefaultAsync(d => d.Id.Equals(dept.DepartmentId));
                doctor.Departments.Add(dpt);
                
               
            }
            await _db.SaveChangesAsync();
            var doctorDto = new DoctorViewDto()
            {
                DoctorId = model.DoctorId,
                DoctorName = docGenDetails.Name,
                Email = docGenDetails.Email,
                PhoneNumber = docGenDetails.PhoneNumber,
                Departments = doctor.Departments
            };
            return doctorDto;

        }

        public async Task<List<DoctorViewDto>> GetAllDoctor()
        {
            var doctorIds = await _db.Doctors.ToListAsync();
            List<DoctorViewDto> doctors = new List<DoctorViewDto>();
            foreach(var doctorId in doctorIds)
            {
                var doctorDetails = await _db.Users.Where(u => u.Id.Equals(doctorId.DoctorId)).FirstOrDefaultAsync();
                if(doctorDetails == null)
                {
                    continue;
                }
                var doc = await _db.Doctors.Where(doc => doc.DoctorId.Equals(doctorId.DoctorId)).Include(d => d.Departments).FirstOrDefaultAsync();
                var depts = doc.Departments;


                var doctor = new DoctorViewDto()
                {
                    DoctorId = doctorId.DoctorId,
                    DoctorName = doctorDetails.Name,
                    Departments = depts,
                    Email = doctorDetails.Email,
                    PhoneNumber = doctorDetails.PhoneNumber,
                };
                doctors.Add(doctor);
            }

            return doctors;

        }

        public async Task<DoctorViewDto> GetDoctorById(string id)
        {
            var doctorDetails = await _db.Users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
            if (doctorDetails == null)
            {
                return null;
            }
            var doc = await _db.Doctors.Where(doc => doc.DoctorId.Equals(id)).Include(d => d.Departments).FirstOrDefaultAsync();
            var depts = doc.Departments;
            var doctor = new DoctorViewDto()
            {
                DoctorId = id,
                DoctorName = doctorDetails.Name,
                Departments = depts,
                Email = doctorDetails.Email,
                PhoneNumber = doctorDetails.PhoneNumber,
            };
            return doctor;
        }
    }
}
