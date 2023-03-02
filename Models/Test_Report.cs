using System.ComponentModel.DataAnnotations;

namespace HMS_API.Models
{
    public class Test_Report
    {
        [Key]
        public Guid TestReportId { get; set; }
        public string Result { get; set; }
        public string Remarks { get; set; }
    }
}
