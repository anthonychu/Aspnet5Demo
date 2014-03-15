namespace Aspnet5Demo.Migrations
{
    using Aspnet5Demo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aspnet5Demo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Aspnet5Demo.Models.ApplicationDbContext";
        }

        protected override void Seed(Aspnet5Demo.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole("Administrator"));

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "admin" };
                userManager.Create(user, "admin321");
                userManager.AddToRole(user.Id, "Administrator");
            }

            if (!context.Events.Any())
            {
                context.Events.Add(new Event
                {
                    Date = new DateTime(2013, 10, 28, 18, 30, 00),
                    Topic = "Building A Custom Big Data Management Application with SharePoint Search",
                    Speaker = "Andy Hopkins"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2013, 11, 7, 18, 30, 0),
                    Topic = "Introducing AngularJS to build Single Page Applications",
                    Speaker = "Jose Marino"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2013, 12, 7, 18, 30, 0),
                    Topic = "Windows Phone DVLUP Day sponsored by NOKIA and BCIT",
                    Speaker = "Nokia"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 1, 16, 18, 30, 0),
                    Topic = "Developing Apps for SharePoint 2013 with Visual Studio 2013",
                    Speaker = "Medhat Elmasry"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 2, 6, 18, 0, 0),
                    Topic = "Oracle and Visual Studio 2013: What's New and Best Practices",
                    Speaker = "Alex Keh"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 3, 11, 18, 30, 0),
                    Topic = "Web Development with ASP.NET MVC, Web API, Visual Studio, and Windows Azure",
                    Speaker = "Anthony Chu"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 4, 3, 18, 30, 0),
                    Topic = "Building Windows Store apps with XAML",
                    Speaker = "Medhat Elmasry"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 5, 6, 18, 30, 0),
                    Topic = "Building Connected Windows 8 Apps with Windows Azure Mobile Services",
                    Speaker = "Gordon Kennett"
                });
                context.Events.Add(new Event
                {
                    Date = new DateTime(2014, 6, 3, 18, 30, 0),
                    Topic = "The Developer Opportunity: Introducing the Windows Store",
                    Speaker = "Nora Sabau"
                });

                context.SaveChanges();

            }
        }
    }
}
