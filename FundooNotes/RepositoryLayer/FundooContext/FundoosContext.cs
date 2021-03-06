using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.FundooContext
{
    public class FundoosContext : DbContext 
    {
        public FundoosContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Label> Label { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }

    }
}
