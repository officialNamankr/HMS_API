using AutoMapper;
using HMS_API.DbContexts;
using HMS_API.Migrations;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HMS_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public DepartmentRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<DepartmentViewDto> AddDepartment(AddDepartmentDto department)
        {
            var newDept = new Department()
            {
                Name = department.Name
            };
            await _db.Departments.AddAsync(newDept);
            await _db.SaveChangesAsync();
            var departmentViewDto = new DepartmentViewDto()
            {
                Id = newDept.Id,
                Name = department.Name,
            };
            return departmentViewDto;
        }
        public async Task<object> EditDepartment(Guid id, EditDepartmentDto model)
        {
            var Department = await _db.Departments.FindAsync(id);
            if (Department == null)
            {
                return null;
            }
            Department.Name = model.Name;
            Department.Name = model.Name;
            _db.SaveChanges();
            return Department;
        }
        public async Task<object> GetDepartmentById(Guid id)
        {
            var department = await _db.Departments.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
            return department;
        }
        public async Task<List<DepartmentViewDto>> GetAllDepartments()
        {
            var depts = await _db.Departments.Where(u => u.IsDeleted.Equals(false)).ToListAsync();

            List<DepartmentViewDto> departments = new List<DepartmentViewDto>();
            foreach (var dept in depts)
            {
                var dep = new DepartmentViewDto { Id = dept.Id, Name = dept.Name };
                departments.Add(dep);
            }
            return departments;
        }
        public async Task<List<DoctorByDepartmentViewDTO>> GetAllDoctorsByDeptId(Guid id)
        {
            //var depts = await _db.Departments.Where(u => u.Id.Equals(id)).Include(Doctor => Doctor.Id).Include(Doctor => Doctor.Name).ToListAsync();
            var dept = await _db.Departments.Where(d => d.Id.Equals(id)).Include(Department => Department.Doctors).FirstOrDefaultAsync();
            

            List<DoctorByDepartmentViewDTO> departments = new List<DoctorByDepartmentViewDTO>();
          
                foreach (var doctor in dept.Doctors)
                {
                    var doctorUser = await _db.Doctors.Where(u => u.DoctorId.Equals(doctor.DoctorId)).Include(Doctor => Doctor.User).FirstOrDefaultAsync();
                    var docs = new DoctorByDepartmentViewDTO { DoctorId = doctorUser.DoctorId, DoctorName = doctorUser.User.Name };
                    departments.Add(docs);

                }
                //var doc = await _db.Doctors.Where(u => u.DoctorId.Equals(doctorId)).Include(Doctor => Doctor.User).FirstOrDefaultAsync();
               
          
            return departments;
        }
        public async Task<bool> DeleteDepartment(Guid id)
        {
            var dept = await _db.Departments.FindAsync(id);
            if (dept == null)
            {
                return true;
            }
            dept.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
