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

        public DbSet<User> user { get; set; }
    }
}
