using BookStore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using System;
using System.Linq;
namespace BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>

    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> categories { get; set; }
        public object ReadingLists { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Username=postgres;Database=bookdb;password=1234; Trust Server Certificate=true",
                options => options.SetPostgresVersion(new Version(11, 13)));

            return new BookStoreContext(optionsBuilder.Options);
        }
    }

}
