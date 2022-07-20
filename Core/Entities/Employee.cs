namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public string? Name { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? JobContractType { get; set; }
        public DateTime StartedIn { get; set; }
        public string? PictureUrl { get; set; }
        public string? FingerPrintUrl { get; set; }
        public string? RfidCode { get; set; }
        public WorkDaySchedule? WorkDaySchedule { get; set; }
        public int WorkDayScheduleId { get; set; }
        
    }
}