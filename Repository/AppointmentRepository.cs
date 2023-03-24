using AutoMapper;
using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace HMS_API.Repository
{
    
    
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public AppointmentRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AddAppointment> AddAppointment(AddAppointment addappointment)
        {
            var patient = await _db.Patients.Where(u => u.PatientId.Equals(addappointment.PatientId)).FirstOrDefaultAsync();
            var doctor = await _db.Doctors.Where(u => u.DoctorId.Equals(addappointment.DoctorId)).FirstOrDefaultAsync();
            var appointment = new Appointment
            {
                Date_Of_Appointment = addappointment.Date_Of_Appointment,
                Time_Of_Appointment = addappointment.Time_Of_Appointment,
                PatientId = addappointment.PatientId,
                Patient = patient,
                DoctorId = addappointment.DoctorId,
                Doctor = doctor
            };
            await _db.Appointments.AddAsync(appointment);
            await _db.SaveChangesAsync();
            
            return addappointment;
        }

        public async Task<bool> CancelAppointment(Guid id)
        {
           var appointment = await _db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return true;
            }
            appointment.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var apps = await _db.Appointments.ToListAsync();

            List<Appointment> appointments = new List<Appointment>();
            foreach (var app in apps)
            {
                var dep = new Appointment { AppointmentId = app.AppointmentId , Date_Of_Appointment = app.Date_Of_Appointment,
                Time_Of_Appointment= app.Time_Of_Appointment , DoctorId= app.DoctorId,PatientId=app.PatientId};
                appointments.Add(dep);
            }
            return appointments;
        }

        public async Task<List<AppointmentDoctorViewDTO>> GetAppointmentByDoctor(string id)
        {
            var appointmentDetails = await _db.Appointments.Where(u => u.DoctorId.Equals(id)).ToListAsync();
            if (appointmentDetails == null)
            {
                return null;
            }
            List<AppointmentDoctorViewDTO> appointments = new List<AppointmentDoctorViewDTO>();
            foreach (var appointment in appointmentDetails)
            {
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.PatientId)).FirstOrDefaultAsync();
                var doc = await _db.Users.Where(d => d.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
                AppointmentDoctorViewDTO app = new AppointmentDoctorViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    PatientId = appointment.PatientId,
                    PatientName = user.Name,
                    Doctorid = appointment.DoctorId,
                    DoctorName = doc.Name
                };
                appointments.Add(app);
            }
            return appointments;
        }

        public async Task<AppointmentAdminViewDTO> GetAppointmentById(Guid id)
        {
            var appointment = await _db.Appointments.Where(u => u.AppointmentId.Equals(id)).FirstOrDefaultAsync();
            if (appointment == null)
            {
                return null;
            }
            var Doctoruser = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
            var Patientuser = await _db.Users.Where(u => u.Id.Equals(appointment.PatientId)).FirstOrDefaultAsync();
            var appointment_ = new AppointmentAdminViewDTO()
            {
                AppointmentId=appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorName = Doctoruser.Name,
                Date_Of_Appointment= appointment.Date_Of_Appointment,
                Time_Of_Appointment= appointment.Time_Of_Appointment,
                DoctorId= appointment.DoctorId,
                PatientName=Patientuser.Name
            };
            return appointment_;
        }

        public async Task<List<AppointmentPatientViewDTO>> GetAppointmentByPatient(string id)
        {
            var appointmentDetails = await _db.Appointments.Where(u => u.PatientId.Equals(id)).ToListAsync();
            if (appointmentDetails == null)
            {
                return null;
            }
            List<AppointmentPatientViewDTO> appointments = new List<AppointmentPatientViewDTO>();
            foreach (var appointment in appointmentDetails)
            {
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
                AppointmentPatientViewDTO app = new AppointmentPatientViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    DoctorId = appointment.DoctorId,
                    DoctorName = user.Name,
                    IsDeleted = appointment.IsDeleted
                };
                appointments.Add(app);
            }
            return appointments;
        }

        public async Task<List<AppointmentPatientViewDTO>> GetCancelledAppointmentByPatient(string id)
        {
            var appointmentDetails = await _db.Appointments.Where(u => u.PatientId.Equals(id)).Where(a => a.IsDeleted).ToListAsync();
            if (appointmentDetails == null)
            {
                return null;
            }
            List<AppointmentPatientViewDTO> appointments = new List<AppointmentPatientViewDTO>();
            foreach (var appointment in appointmentDetails)
            {
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
                AppointmentPatientViewDTO app = new AppointmentPatientViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    DoctorId = appointment.DoctorId,
                    DoctorName = user.Name,
                    IsDeleted = appointment.IsDeleted
                };
                appointments.Add(app);
            }
            return appointments;
        }


        public async Task<List<AppointmentPatientViewDTO>> GetPastAppointmentByPatient(string id)
        {
            var dateOnly = DateOnly.FromDateTime(DateTime.Now);
            var timeOnly = TimeOnly.FromDateTime(DateTime.Now);
            
            var appointmentDetails = await _db.Appointments.Where(u => u.PatientId.Equals(id) && !u.IsDeleted && (u.Date_Of_Appointment < dateOnly)||(u.Date_Of_Appointment == dateOnly && u.Time_Of_Appointment < timeOnly && !u.IsDeleted)).ToListAsync();
            if (appointmentDetails == null)
            {
                return null;
            }
            List<AppointmentPatientViewDTO> appointments = new List<AppointmentPatientViewDTO>();
            foreach (var appointment in appointmentDetails)
            {
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
                AppointmentPatientViewDTO app = new AppointmentPatientViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    DoctorId = appointment.DoctorId,
                    DoctorName = user.Name,
                    IsDeleted = appointment.IsDeleted
                };
                appointments.Add(app);
            }
            return appointments;
        }



        public async Task<List<AppointmentPatientViewDTO>> GetUpcomingAppointmentByPatient(string id)
        {
            var dateOnly = DateOnly.FromDateTime(DateTime.Now);
            var timeOnly = TimeOnly.FromDateTime(DateTime.Now);

            var appointmentDetails = await _db.Appointments.Where(u => u.PatientId.Equals(id) && !u.IsDeleted && (u.Date_Of_Appointment > dateOnly) || (u.Date_Of_Appointment == dateOnly && u.Time_Of_Appointment > timeOnly && !u.IsDeleted)).ToListAsync();
            if (appointmentDetails == null)
            {
                return null;
            }
            List<AppointmentPatientViewDTO> appointments = new List<AppointmentPatientViewDTO>();
            foreach (var appointment in appointmentDetails)
            {
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
                AppointmentPatientViewDTO app = new AppointmentPatientViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    DoctorId = appointment.DoctorId,
                    DoctorName = user.Name,
                    IsDeleted = appointment.IsDeleted
                };
                appointments.Add(app);
            }
            return appointments;
        }
        public async Task<List<TimeOnly>> GetTimeByDateAndDoctorId(Guid doctorId,DateOnly date)
        {
            var appointmentDetails = await _db.Appointments.Where(u => u.DoctorId.Equals(doctorId) && u.Date_Of_Appointment.Equals(date)).ToListAsync();
            List<TimeOnly> timeOnly = new List<TimeOnly>();
            foreach (var appointment in appointmentDetails)
            {
                timeOnly.Add(appointment.Time_Of_Appointment);
            }
            return timeOnly;

            
        }
    }
}
