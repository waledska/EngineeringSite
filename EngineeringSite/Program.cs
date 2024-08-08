using EngineeringSite.Areas.Control_panel.Controllers;
using EngineeringSite.Data;
using EngineeringSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//var connectionStringEngSite = builder.Configuration.GetConnectionString("engineeringConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<engineeringContext>(options =>
    //options.UseSqlServer(connectionStringEngSite));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

///////////////////////// identity config
//builder.Services.AddIdentity<ApplicationUser, IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Configure Identity options here
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<ApplicationUser>();
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// for stop validation for the register for new user's password
builder.Services.Configure<IdentityOptions>(x => {
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireDigit = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


//app.UseMVC(routes =>
//{
//    routes.MapRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//        );
//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Control_panel",
        pattern: "{area:exists}/{controller=Admin_Panel}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "Control_panel",
        pattern: "{area:exists}/{controller=Admin_Panel}/{action=Index}/{userId?}/{roleId?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});

// for creating at the first the roles in the database
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// for creating at begining the first user in the system and give him role admin 
using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@admin.com";
    string password = "Test1234, ";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    var users = userManager.Users.ToList();
    foreach (var user in users)
    {
        if(user.Email != "admin@admin.com")
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}


app.MapRazorPages();

app.Run();
