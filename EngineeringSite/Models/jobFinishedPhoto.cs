using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringSite.Models
{
    public class jobFinishedPhoto
    {
            public int Id { get; set; }
            public string FileName { get; set; }

            [ForeignKey("jobsFinished")]
            public int jobsFinishedId { get; set; }
            public jobsFinished job { get; set; }
        
    }
}
