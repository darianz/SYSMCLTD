
using Microsoft.EntityFrameworkCore;



namespace SYSMCLTDR.Models
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Contacts> Contacts { get; set; }



    }
}
