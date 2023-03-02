using AutoMapper;
using HMS_API.DbContexts;
using HMS_API.Migrations;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
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

        public async Task<List<DepartmentViewDto>> GetAllDepartments()
        {
            var depts = await _db.Departments.ToListAsync();

            List<DepartmentViewDto> departments = new List<DepartmentViewDto>();
            foreach (var dept in depts)
            {
                var dep = new DepartmentViewDto { Id = dept.Id, Name = dept.Name };
                departments.Add(dep);
            }
            return departments;
        }
    }
}
