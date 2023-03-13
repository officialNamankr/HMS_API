using AutoMapper;
using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Models.Dto.GetDtos;
using HMS_API.Models.Dto.PostDtos;
using HMS_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
<<<<<<< HEAD
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.PatientId)).FirstOrDefaultAsync();
=======
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
                AppointmentDoctorViewDTO app = new AppointmentDoctorViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    PatientId = appointment.PatientId,
<<<<<<< HEAD
                    PatientName = user.Name
=======
                    PatientName = appointment.Patient.User.Name
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
                };
                appointments.Add(app);
            }
            return appointments;
        }

        public async Task<AppointmentAdminViewDTO> GetAppointmentById(string id)
        {
            var AppointmentDetails = await _db.Appointments.Where(u => u.AppointmentId.Equals(id)).FirstOrDefaultAsync();
            if (AppointmentDetails == null)
            {
                return null;
            }


            var appointment = new AppointmentAdminViewDTO()
            {
                AppointmentId=AppointmentDetails.AppointmentId,
                PatientId = AppointmentDetails.PatientId,
                DoctorName = AppointmentDetails.Doctor.User.Name,
                Date_Of_Appointment=AppointmentDetails.Date_Of_Appointment,
                Time_Of_Appointment=AppointmentDetails.Time_Of_Appointment,
                DoctorId=AppointmentDetails.DoctorId,
                PatientName=AppointmentDetails.Patient.User.Name
            };
            return appointment;
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
<<<<<<< HEAD
                var user = await _db.Users.Where(u => u.Id.Equals(appointment.DoctorId)).FirstOrDefaultAsync();
=======
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
                AppointmentPatientViewDTO app = new AppointmentPatientViewDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    Date_Of_Appointment = appointment.Date_Of_Appointment,
                    Time_Of_Appointment = appointment.Time_Of_Appointment,
                    DoctorId = appointment.DoctorId,
<<<<<<< HEAD
                    DoctorName = user.Name
=======
                    DoctorName = appointment.Doctor.User.Name
>>>>>>> 5daa595f913dc03b44276c08edc1c126652d31e5
                };
                appointments.Add(app);
            }
            return appointments;
        }
    }
}
