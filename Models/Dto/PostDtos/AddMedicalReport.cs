namespace HMS_API.Models.Dto.PostDtos
{
    public class AddMedicalReport
    {
        public Guid AppointmentId { get; set; }
        public string Remarks { get; set; }
        public ICollection<AddRecommentTestsToMedicalReport> RecommendedTests { get; set; }
    }
}
