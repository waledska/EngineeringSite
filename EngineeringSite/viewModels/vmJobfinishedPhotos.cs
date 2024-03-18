using EngineeringSite.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringSite.viewModels
{
    public class vmJobfinishedPhotos
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int jobsFinishedId { get; set; }
        public jobsFinished job { get; set; }
        public IFormFile formFile { get; set; }

    }
}
