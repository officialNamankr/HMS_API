using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models
{
    public class RecommendedTest
    {
        public RecommendedTest()
        {
            this.Tests = new HashSet<Test>();
        }
        [Key]
        public Guid RTId { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<Test_Report> Reports { get; set; }
    }
}
