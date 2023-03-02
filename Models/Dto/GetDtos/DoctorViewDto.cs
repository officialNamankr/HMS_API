namespace HMS_API.Models.Dto.GetDtos
{
    public class DoctorViewDto
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
