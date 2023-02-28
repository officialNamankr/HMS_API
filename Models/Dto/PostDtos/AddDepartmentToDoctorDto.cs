namespace HMS_API.Models.Dto.PostDtos
{
    public class AddDepartmentToDoctorDto
    {
        public string DoctorId { get; set; }
        public ICollection<DepartmentIdDto> DepartmentIds { get; set; }
    }
}
