using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;
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
           
            if(model.RecommendedTests != null)
            {
                foreach(var tst in model.RecommendedTests)
                {
                    var test = await _db.Tests.FindAsync(tst.TestId);
                    if(test != null)
                    {
                        
                        mdReport.RecommendedTest.Tests.Add(test);
                    }
                   
                }
               
            }
            await _db.Medical_Reports.AddAsync(mdReport);
            await _db.SaveChangesAsync();
            return mdReport;
        }
        public async Task<object> EditReport(Guid id, EditReportDTO model)
        {
            var Report = await _db.Medical_Reports.FindAsync(id);
            if (Report == null)
            {
                return null;
            }
            Report.Remarks = model.Remarks;
            foreach (var ids in model.RecommendedTest.TestIds)
            {
                var test= await _db.Tests.FindAsync(ids);
                Report.RecommendedTest.Tests.Add(test);
            }
         
            await _db.SaveChangesAsync();
            return Report;
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
