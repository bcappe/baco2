
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterEntryDto
    {
        [Required]
        public string Rfid { get; set; } 
        [Required]
        public DateTime TimeStamp { get; set; } 
        [Required]
        public bool IsIn { get; set; }

    }
}