using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace Aspnet5Demo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }

        public ICollection<Event> Events { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Speaker { get; set; }
        public DateTime Date { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasMany(e => e.Users).WithMany(u => u.Events);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; }
    }
}