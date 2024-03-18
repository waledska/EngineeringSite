using Microsoft.EntityFrameworkCore;

namespace EngineeringSite.Models
{
    public class engineeringContext : DbContext 
    {
        public engineeringContext(DbContextOptions<engineeringContext> options)
            : base(options)
        {
        }
        public DbSet<jobsFinished> jobsFinisheds { get; set; }
        public DbSet<message> messages { get; set; }
        public DbSet<service> services { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<jobFinishedPhoto> jobFinishedPhotos { get; set; }
        public DbSet<review> reviews { get; set; }

    }
}
