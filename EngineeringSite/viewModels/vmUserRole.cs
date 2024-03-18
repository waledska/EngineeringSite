using Microsoft.AspNetCore.Identity;

namespace EngineeringSite.viewModels
{
    public class vmUserRole
    {
        public IdentityUser user { get; set; }
        public IdentityRole role { get; set; }
    }
}
