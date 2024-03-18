using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringSite.Models
{
    public class jobsFinished
    {
        public int id { get; set; }
        public string jobName { get; set; }
        public string? description { get; set; }
        public DateTime? date { get; set; }
        public string? location { get; set; }

        [ForeignKey("service")]
        public int serviceId { get; set; }

        public service service { get; set; }
        public List<jobFinishedPhoto> photo { get; set; }
        [NotMapped]
        public List<IFormFile>? formFile { get; set; }

    }
}
