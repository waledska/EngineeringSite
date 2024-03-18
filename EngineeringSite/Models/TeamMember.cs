using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringSite.Models
{
    public class TeamMember
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? facebookLink { get; set; }
        public string? linkedinLink { get; set; }
        public string? photo { get; set; }
        public string? job { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}
