using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ERMS.Domain.Entities;

namespace ERMS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestAttachment> RequestAttachments { get; set; }
        public DbSet<RequestComment> RequestComments { get; set; }
        public DbSet<RequestHistory> RequestHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Approval> Approvals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


        }
    }
}
