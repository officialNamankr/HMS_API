using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models
{
    public class Test
    {
        [Key]
        public Guid TestId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RecommendedTest> RecommendedTests { get; set;}
    }
}
