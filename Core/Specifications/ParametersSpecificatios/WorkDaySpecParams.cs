namespace Core.Specifications
{
    public class WorkDaySpecParams
    {
        public WorkDaySpecParams()
        {
        }
        public int? Id { get; set; }

        public DateTime? Date { get; set; }

        public int? WorkDayScheduleID { get; set; }
        
        public int? EmployeeID {get; set;}
    }
}