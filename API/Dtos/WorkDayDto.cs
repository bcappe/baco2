namespace API.Dtos
{
    public class WorkDayDto
    {
        public string? Description { get; set; }    

        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut{ get; set; }

        public DateTime? LunchTimeIn{ get; set; }
        public DateTime? LunchTimeOut{ get; set; }

    }
}