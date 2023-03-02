using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HMS_API.Models;

namespace HMS_API.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Medical_Report> Medical_Reports { get; set;}

        public DbSet<Test_Report> Test_Reports { get;set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<RecommendedTest> Recommended_Tests { get; set; }
    }
}
