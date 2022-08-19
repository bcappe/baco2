//TODO: remover o constrututor e implementar uma spec
namespace Core.Entities
{
    public class WorkDay : WorkDayBase
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }    
        public Employee? Employee { get; set; }  
        public int EmployeeId { get; set; } 

        public float? ExtraHours { get; set; }

    }
}