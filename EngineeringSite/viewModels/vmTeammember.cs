namespace EngineeringSite.viewModels
{
    public class vmTeammember
    {
        public int id { get; set; }
        public string name { get; set; }
        public string facebookLink { get; set; }
        public string linkedinLink { get; set; }
        public string photo { get; set; }
        public string job { get; set; }
        public IFormFile formFile { get; set; }
    }
}
