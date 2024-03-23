using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Haver_Niagara.Data
{
    public static class ApplicationDbInitializer //followed from video / took from previous mvc course
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                //Create Roles
                var RoleManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Admin", "Supervisor", "Quality Representative",  //Role Names a User can be assigned
                                       "Engineer", "Procurement", "Operations" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                //Create Users
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

       
                //Admin
                if (userManager.FindByEmailAsync("admin@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "admin@outlook.com",
                        Email = "admin@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result; //If you are going to set a custom password,
                                                                                              //Is must not break the constraints found in program.cs
                    if (result.Succeeded)                                                     //for simplicity I disabled some constraints
                    {                                                                         //so instead of having to use something like P@5w0rd! just use password
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
                //Supervisor
                if (userManager.FindByEmailAsync("super@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "super@outlook.com",
                        Email = "super@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Supervisor").Wait();
                    }
                }
                //Regular account which probably wont be needed
                if (userManager.FindByEmailAsync("user@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "user@outlook.com",
                        Email = "user@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;
                    //Not in any role
                }
                //Quality Representative Stuff
                if (userManager.FindByEmailAsync("qualityrepresentative@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "qualityrepresentative@outlook.com",
                        Email = "qualityrepresentative@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Quality Representative").Wait();
                    }
                }
                //Engineer
                if (userManager.FindByEmailAsync("engineer@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "engineer@outlook.com",
                        Email = "engineer@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Engineer").Wait();
                    }
                }
                //Procurement
                if (userManager.FindByEmailAsync("procurement@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "procurement@outlook.com",
                        Email = "procurement@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Procurement").Wait();
                    }
                }

                //Operations Stuff
                if (userManager.FindByEmailAsync("operations@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "operations@outlook.com",
                        Email = "operations@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Operations").Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
