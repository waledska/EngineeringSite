using EngineeringSite.Data;
using EngineeringSite.Models;
using EngineeringSite.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Identity;


namespace EngineeringSite.Areas.Control_panel.Controllers
{


    [Area("Control_panel")]
    public class Admin_PanelController : Controller
    {

        private readonly ILogger<Admin_PanelController> _logger;
        private readonly engineeringContext db;
        public result result = new result();
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IWebHostEnvironment host;
        UserManager<IdentityUser> userManager;
        //private readonly UserManager<ApplicationUser> userManager;

        public Admin_PanelController(ILogger<Admin_PanelController> logger, engineeringContext _context, RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager, IWebHostEnvironment _host, UserManager<IdentityUser> user  /*, UserManager<ApplicationUser> _userManager*/)
        {
            _logger = logger;
            db = _context;
            RoleManager = roleManager;
            result.Listservice = db.services.ToList();
            _signInManager = signInManager;
            host = _host;
            userManager = user;
            //userManager = _userManager;
        }

        // GET: Admin_PanelController
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                // view bag for the layout numOfMessages/userName
                ViewBag.DataToLayout = new
                {
                    userName = User.Identity.Name,
                    numMessages = db.messages.Count()
                };
                return RedirectToAction("Messages", "Admin_Panel", new { area = "Control_panel" });
            }
            else
            {
                return View();
            }
        }

        // for logining out
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout_admin()
        {
            await _signInManager.SignOutAsync();
            // Redirect to a desired page after logout.
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Messages()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            // get messages


            return View(db.messages.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Reviews()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            // get messages


            return View(db.reviews.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult TeamMembers()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            return View(db.TeamMembers.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OurWork()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            return View(db.jobsFinisheds.ToList());
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Users()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            var users = userManager.Users.ToList();

            // to give any new user role'user' at the first
            foreach (var userr in users)
            {
                if (!(await userManager.IsInRoleAsync(userr, "Admin")))
                {
                    await userManager.AddToRoleAsync(userr, "User");
                }
            }

            // Get the current user
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);


            List<vmUserRole> vmUserRoles = new List<vmUserRole>();
            var Role = new IdentityRole();

            // to get every user with him role
            foreach (var user in users)
            {
                var roleName = await userManager.GetRolesAsync(user);
                Role = await RoleManager.FindByNameAsync(roleName[0]);
                vmUserRoles.Add(new vmUserRole
                {
                    user = user,
                    role = Role
                });
            }

            ViewBag.Roles = RoleManager.Roles.ToList();

            return View(vmUserRoles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string userid, string roleid)
        {
            // Find the user and the role
            var userID = _signInManager.UserManager.GetUserIdAsync;
            var user = await userManager.FindByIdAsync(userid);
            var role = await RoleManager.FindByIdAsync(roleid);

            if (user != null && role != null)
            {
                // Remove the user from existing roles
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    await userManager.RemoveFromRoleAsync(user, userRole);
                }

                // Add the user to the new role
                await userManager.AddToRoleAsync(user, role.Name);
            }

            return RedirectToAction("Users", "Admin_Panel", new { area = "Control_panel" });
        }



        [Authorize(Roles = "Admin")]
        public ActionResult JobFinished_add()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult TeamMembers_add()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult JobFinished_modify(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            ViewBag.services = db.services.ToList();

            // get jobfinished
            var jobAndPhotos = db.jobsFinisheds.Include(x => x.photo)
                .FirstOrDefault(i => i.id == id);

            return View(jobAndPhotos);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult TeamMembers_modify(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            var member = db.TeamMembers.Find(id);

            // for empty links
            if (member.facebookLink == "ل")
            {
                member.facebookLink = "لا يوجد لينك لهذا المستخدم";
            }
            if (member.linkedinLink == "ل")
            {
                member.linkedinLink = "لا يوجد لينك لهذا المستخدم";
            }
            return View(member);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult JobFinished_details(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            // get the job with it's photos
            var jobAndPhotos = db.jobsFinisheds.Include(x => x.photo)
                .FirstOrDefault(i => i.id == id);

            return View(jobAndPhotos);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult TeamMembers_details(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            var member = db.TeamMembers.Find(id);

            // for empty links
            if (member.facebookLink == "ل")
            {
                member.facebookLink = "لا يوجد لينك لهذا المستخدم";
            }
            if (member.linkedinLink == "ل")
            {
                member.linkedinLink = "لا يوجد لينك لهذا المستخدم";
            }
            return View(member);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Messages_details(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            // get the message for it's details 

            return View(db.messages.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult delete_Message(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            db.messages.Remove(db.messages.Find(id));
            db.SaveChanges();

            return RedirectToAction("Messages", "Admin_Panel", new { area = "Control_panel" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RevVisibility(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            var rev = db.reviews.Find(id);
            if (rev != null)
            {
                if (rev.showReview == true)
                {
                    rev.showReview = false;
                }
                else
                {
                    rev.showReview = true;
                }
            }

            db.SaveChanges();

            return RedirectToAction("Reviews", "Admin_Panel", new { area = "Control_panel" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult delete_JobFinished(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            db.jobsFinisheds.Remove(db.jobsFinisheds.Find(id));
            db.SaveChanges();

            return RedirectToAction("OurWork", "Admin_Panel", new { area = "Control_panel" });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult delete_TeamMembers(int id)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            db.TeamMembers.Remove(db.TeamMembers.Find(id));
            db.SaveChanges();

            return RedirectToAction("TeamMembers", "Admin_Panel", new { area = "Control_panel" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add_member()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add_project()
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            ViewBag.services = db.services.ToList();
            return View();
        }

        // ============== post end points

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult addNewMember(TeamMember member)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()

            };
            if (ModelState.IsValid)
            {
                try
                {
                    uploadphoto_Member(member);
                    db.TeamMembers.Add(member);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return RedirectToAction("TeamMembers", "Admin_Panel", new { area = "Control_panel" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateMember(TeamMember member)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()

            };
            if (ModelState.IsValid)
            {
                try
                {
                    uploadphoto_Member(member);
                    db.TeamMembers.Update(member);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return RedirectToAction("TeamMembers", "Admin_Panel", new { area = "Control_panel" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult addNewProject(jobsFinished job)
        {
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };

            try
            {
                // Save the job to get its ID
                db.jobsFinisheds.Add(job);
                db.SaveChanges();

                // Now that the job has been saved, get its ID
                int jobId = job.id;

                // Upload photos associated with the job
                uploadphotos_project(jobId, job.formFile);

            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("OurWork", "Admin_Panel", new { area = "Control_panel" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateProject(jobsFinished job)
        {
            // view bag for the layout numOfMessages/userName
            ViewBag.DataToLayout = new
            {
                userName = User.Identity.Name,
                numMessages = db.messages.Count()
            };
            try
            {
                // Upload photos associated with the job
                uploadphotos_Updateproject(job.id, job.formFile);

                // Save the job to get its ID
                db.jobsFinisheds.Update(job);
                db.SaveChanges(); ;


            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("OurWork", "Admin_Panel", new { area = "Control_panel" });
        }

        // uploading photos!
        public void uploadphoto_Member(TeamMember member)
        {
            if (member.formFile != null)
            {
                string bathUntilPhotosFile = Path.Combine(host.WebRootPath, "images");
                string uniuePhotoName = Guid.NewGuid() + ".jpg";
                string newPhotoPath = Path.Combine(bathUntilPhotosFile, uniuePhotoName);

                using (var fileStream = new FileStream(newPhotoPath, FileMode.Create))
                {
                    member.formFile.CopyTo(fileStream);
                }
                member.photo = uniuePhotoName;
            }
        }

        public void uploadphotos_project(int jobId, List<IFormFile> formFiles)
        {
            if (formFiles != null && formFiles.Any())
            {
                string bathUntilPhotosFile = Path.Combine(host.WebRootPath, "images");
                foreach (var file in formFiles)
                {
                    string uniquePhotoName = Guid.NewGuid() + ".jpg";
                    string newPhotoPath = Path.Combine(bathUntilPhotosFile, uniquePhotoName);

                    using (var fileStream = new FileStream(newPhotoPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    // Create a new jobFinishedPhoto associated with the job ID
                    var photoForJob = new jobFinishedPhoto
                    {
                        jobsFinishedId = jobId,
                        FileName = uniquePhotoName
                    };
                    // Add the photo to the database
                    db.jobFinishedPhotos.Add(photoForJob);
                }
                db.SaveChanges();
            }
        }

        public void uploadphotos_Updateproject(int jobId, List<IFormFile> formFiles)
        {
            if (formFiles != null && formFiles.Any())
            {
                string bathUntilPhotosFile = Path.Combine(host.WebRootPath, "images");
                var photosToDelete = db.jobFinishedPhotos.Where(photo => photo.jobsFinishedId == jobId).ToList();

                foreach (var photo in photosToDelete)
                {
                    db.jobFinishedPhotos.Remove(photo);
                }

                foreach (var file in formFiles)
                {
                    string uniquePhotoName = Guid.NewGuid() + ".jpg";
                    string newPhotoPath = Path.Combine(bathUntilPhotosFile, uniquePhotoName);

                    using (var fileStream = new FileStream(newPhotoPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    // Create a new jobFinishedPhoto associated with the job ID
                    var photoForJob = new jobFinishedPhoto
                    {
                        jobsFinishedId = jobId,
                        FileName = uniquePhotoName
                    };
                    // Add the photo to the database
                    db.jobFinishedPhotos.Add(photoForJob);
                }
                db.SaveChanges();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
