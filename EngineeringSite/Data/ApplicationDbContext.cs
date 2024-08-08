using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EngineeringSite.Areas.Control_panel.Controllers;
using Microsoft.EntityFrameworkCore;
using EngineeringSite.Models;

namespace EngineeringSite.Data
{
    public class ApplicationDbContext : IdentityDbContext//<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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