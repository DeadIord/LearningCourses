using LearningCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LearningCourses.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Tests> Tests { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);// Задает схему для базы данных
            builder.HasDefaultSchema("Identity");   /* Переименовывает таблицу пользователей из AspNetUsers в Identity.User. */
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });


            builder.Entity<ApplicationUser>()
                 .HasMany(h => h.Material)
                 .WithOne(v => v.ApplicationUser)
                 .HasForeignKey(v => v.ApplicationUserId);

            builder.Entity<ApplicationUser>()
                .HasMany(h => h.Grades)
                .WithOne(v => v.ApplicationUser)
                .HasForeignKey(v => v.ApplicationUserId);

            builder.Entity<Material>()
               .HasMany(h => h.Tests)
               .WithOne(v => v.Material)
               .HasForeignKey(v => v.MaterialId);

            builder.Entity<Tests>()
              .HasMany(h => h.Questions)
              .WithOne(v => v.Tests)
              .HasForeignKey(v => v.QuestionId);

            builder.Entity<Questions>()
             .HasMany(h => h.Answers)
             .WithOne(v => v.Questions)
             .HasForeignKey(v => v.QuestionId);

            builder.Entity<Tests>()
             .HasMany(h => h.Grades)
             .WithOne(v => v.Tests)
             .HasForeignKey(v => v.TestId);
        }
    }
}
