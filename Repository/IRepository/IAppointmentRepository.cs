﻿using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;

namespace HMS_API.Repository.IRepository
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<AppointmentAdminViewDTO> GetAppointmentById(string id);
        Task<List<AppointmentDoctorViewDTO>> GetAppointmentByDoctor(string id);
        Task<List<AppointmentPatientViewDTO>> GetAppointmentByPatient(string id);
        Task<AddAppointment> AddAppointment(AddAppointment appointment );

    }
}
