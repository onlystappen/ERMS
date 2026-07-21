using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ERMS.Domain.Entities;
using ERMS.Application.Common.Interfaces;

namespace ERMS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
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
        public DbSet<AuditLog> AuditLogs { get; set; } // <-- CS0535 hatasını çözen DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(a => a.AuditLogId);
                entity.Property(a => a.Action).IsRequired().HasMaxLength(100);
                entity.Property(a => a.EntityName).IsRequired().HasMaxLength(100);
            });
        }
    }
}