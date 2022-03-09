using BabyMemory.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }

        public DbSet<Children> Childrens { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<HealthProcedure> HealthProcedures { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Memory> Memories { get; set; }
        
        public DbSet<News> News { get; set; }
    }
}