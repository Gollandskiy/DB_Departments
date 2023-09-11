using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_
{
    public class DataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Manager> Managers {get; set; }
        public DataContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ado-ef-p12;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().
                HasOne(m => m.MainDep).
                WithMany().
                HasForeignKey(m => m.IdMainDep).
                HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<Manager>().HasOne(m => m.SecDep).
                WithMany().
                HasForeignKey(m => m.IdSecDep).
                HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<Manager>().
                HasOne(m => m.Chief).
                WithMany().
                HasForeignKey(m => m.IdChief).
                HasPrincipalKey(d => d.Id);
        }

    }
}
