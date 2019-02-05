namespace AspNetIdentity.WebApi.Migrations
{
    using AspNetIdentity.WebApi.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetIdentity.WebApi.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspNetIdentity.WebApi.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user001 = new ApplicationUser()
            {
                UserName = "AshokanS",
                Email = "v-assiva@microsoft.com",
                EmailConfirmed = true,
                FirstName = "Ashokan",
                LastName = "Sivapragasam",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user001, "MySuperP@ss!");

            var user002 = new ApplicationUser()
            {
                UserName = "User002",
                Email = "user002@microsoft.com",
                EmailConfirmed = true,
                FirstName = "User",
                LastName = "002",
                Level = 3,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user002, "MySuperP@ss!");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin"});
                roleManager.Create(new IdentityRole { Name = "User"});
            }

            var adminUser = manager.FindByName(user001.UserName);
            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            var normalUser = manager.FindByName(user002.UserName);
            manager.AddToRoles(normalUser.Id, new string[] { "User" });
        }
    }
}
