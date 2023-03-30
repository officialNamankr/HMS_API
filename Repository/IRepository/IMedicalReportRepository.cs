using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Models.Dto.PutDtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HMS_API.Repository.IRepository
{
    public interface IMedicalReportRepository
    {
        Task<ViewMedicalReport> GetReportByAppointmentId(Guid id);
        Task<Medical_Report> AddMedicalReport(AddMedicalReport report);
        Task<object> EditReport(Guid id, EditReportDTO model);

    }
}
