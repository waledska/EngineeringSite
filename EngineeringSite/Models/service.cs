using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringSite.Models
{
    public class service
    {
        public int id { get; set; }
        public string serviceName { get; set; }
        public string photo { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public  List<jobsFinished> jobsFinisheds { get; set; }
    }
}
