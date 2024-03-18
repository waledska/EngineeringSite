using EngineeringSite.Models;

namespace EngineeringSite.viewModels
{
    public class result
    {
        // objects
        public jobsFinished jobsFinished { get; set; }
        public  message message { get; set; }
        public  service service { get; set; }
        public  TeamMember TeamMember { get; set; }
        // lists
        public  List<jobsFinished> ListjobsFinished { get; set; }
        public List<message> Listmessage { get; set; }
        public List<service> Listservice { get; set; }
        public List<TeamMember> ListTeamMember { get; set; }
    }
}
