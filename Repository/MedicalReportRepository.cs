using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
                AppointmentId = model.AppointmentId,
            };
            await _db.Medical_Reports.AddAsync(mdReport);
            await _db.SaveChangesAsync();
            return mdReport;
        }

        public async Task<ViewMedicalReport> GetReportByAppointmentId(Guid id)
        {
            var Mdreport = await _db.Medical_Reports.Where(a => a.AppointmentId.Equals(id)).FirstOrDefaultAsync();
            if(Mdreport == null) { return null; }
            var appointmentDetails = await _db.Appointments.FindAsync(Mdreport.AppointmentId); 
            var Patient = await _db.Users.FindAsync(appointmentDetails.PatientId);
            var PatientName = Patient.Name;
            var Doctor = await _db.Users.FindAsync(appointmentDetails.DoctorId);
            var DoctorName = Doctor.Name;
            var Report = new ViewMedicalReport
            {
                AppointmentId = id,
                DateOfExamination = appointmentDetails.Date_Of_Appointment,
                TimeOfExamination = appointmentDetails.Time_Of_Appointment,
                Remarks = Mdreport.Remarks,
                DoctorName = DoctorName,
                PatientName = PatientName,
                MedicalReportId = Mdreport.MedicalReportId,
            };
            return Report;
        }
    }
}
