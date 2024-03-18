using System.ComponentModel.DataAnnotations;

namespace EngineeringSite.Models
{
    public class message
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Message content is required.")]
        [MaxLength(500, ErrorMessage = "Max size for the message only 500 char!")]
        public string Message { get; set; }
        public DateTime time { get; set; }
    }
}
