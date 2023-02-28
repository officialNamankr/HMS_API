namespace HMS_API.Models
{
    public class Department
    {
        public Department()
        {
            this.Doctors = new HashSet<Doctor>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
