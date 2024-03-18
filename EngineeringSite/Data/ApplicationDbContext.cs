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
    }
}