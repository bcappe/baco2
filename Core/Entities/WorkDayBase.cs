namespace Core.Entities
{
    public class WorkDayBase: BaseEntity
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut{ get; set; }

        public DateTime? LunchTimeIn{ get; set; }
        public DateTime? LunchTimeOut{ get; set; }

        public float? WorkDayDuration{ get; set; }

    }
}