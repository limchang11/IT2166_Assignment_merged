using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IT2166_Assignment.Models;

namespace IT2166_Assignment.Models
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<IT2166_Assignment.Models.ProjectRole> ProjectRole { get; set; }
        public DbSet<IT2166_Assignment.Models.Booking> Bookings { get; set; }
        public virtual DbSet<Product> Products { get; set; }

    }
}
