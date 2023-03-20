using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;

namespace HMS_API.Repository
{
    public class MedicalReportRepository : IMedicalReportRepository
    {
        private readonly ApplicationDbContext _db;

        public MedicalReportRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Medical_Report> AddMedicalReport(AddMedicalReport model)
        {
            var mdReport = new Medical_Report
            {
                Remarks = model.Remarks,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                AppointmentId = model.AppointmentId,
            };
            await _db.Medical_Reports.AddAsync(mdReport);
            await _db.SaveChangesAsync();
            return mdReport;
        }

        public async Task<ViewMedicalReport> GetReportByAppointmentId(Guid id)
        {
            var Mdreport = await _db.Medical_Reports.FindAsync(id);
            if(Mdreport == null) { return null; }   
            var Patient = await _db.Users.FindAsync(Mdreport.PatientId);
            var PatientName = Patient.Name;
            var Doctor = await _db.Users.FindAsync(Mdreport.DoctorId);
            var DoctorName = Doctor.Name;
            var Report = new ViewMedicalReport
            {
                AppointmentId = id,
                DateTimeOfExamination = Mdreport.DateTimeOfExamination,
                Remarks = Mdreport.Remarks,
                DoctorName = DoctorName,
                PatientName = PatientName,
                MedicalReportId = Mdreport.MedicalReportId,
            };
            return Report;
        }
    }
}
