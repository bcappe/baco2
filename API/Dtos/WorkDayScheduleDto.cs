using System.ComponentModel.DataAnnotations;
namespace API.Dtos
{
    public class WorkDayScheduleDto
    {
        public string? Name { get; set; }    

        [Required]
        public DateTime? CheckIn { get; set; }
        [Required]
        public DateTime? CheckOut{ get; set; }
        [Required]
        public DateTime? LunchTimeIn{ get; set; }
        [Required]
        public DateTime? LunchTimeOut{ get; set; }
    }
}