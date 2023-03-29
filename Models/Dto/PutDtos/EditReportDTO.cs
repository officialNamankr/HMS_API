using Microsoft.Build.Framework;

namespace HMS_API.Models.Dto.PutDtos
{
    public class EditReportDTO
    {
        [Required]
        public string Remarks { get; set; }
        public  EditRecommendedTestDTO RecommendedTest { get; set; }
 
        
    }
}
