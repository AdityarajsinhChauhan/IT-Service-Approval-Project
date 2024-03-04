using Azure.Core;
using Microsoft.EntityFrameworkCore;
namespace ITServiceApprovalProject
{
    public class employeeContext:DbContext
    {
        public DbSet<employee> employee { get; set; }
        public DbSet<credentials> credentials { get; set; }
        public DbSet<request> request { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ITServiceDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<employee>().ToTable("employee");
            modelBuilder.Entity<credentials>().ToTable("credentials");
            modelBuilder.Entity<request>().ToTable("request");

            modelBuilder.Entity<credentials>()
                .HasOne(c => c.employee)
                .WithMany()
                .HasForeignKey(c => c.employeeId);

           

        }

    }
}
