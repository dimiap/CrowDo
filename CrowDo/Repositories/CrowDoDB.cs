using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrowDo.Repositories
{
    public class CrowDoDB : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Funding> Fundings { get; set; }
        public DbSet<Packages> Packages { get; set; }

        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            string s1 = "Server =DESKTOP-2M111GB; Database =CrowDoDB; Integrated Security=SSPI;Persist Security Info=False;";
            optionsBuilder.UseSqlServer(s1);
        }
    }
}
