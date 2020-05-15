using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Models
{
    public class Cw10DbContext : DbContext
    {

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public Cw10DbContext()
        {


        }

        public Cw10DbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasKey(e => e.IndexNumber);

            modelBuilder.Entity<Student>().Property(e => e.FirstName).HasMaxLength(100).IsRequired();


        }





        }
}
