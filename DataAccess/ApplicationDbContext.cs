using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
           new Category { ID = 1, CategoryName = "Valentine's Day" },
           new Category { ID = 2, CategoryName = "Birthday" },
            new Category { ID = 3, CategoryName = "Symphaty" },
           new Category { ID = 4, CategoryName = "All Occasions" },
            new Category { ID = 5, CategoryName = "Gifts" }
            );
            builder.Entity<Product>().HasData(
             new Product
             {
                 ID = 2,
                 Name = "Valentine's Day Red Roses",
                 Price = 200,
                 Discount = 150,
                 CategoryId = 1
             }


              );

        }
    }

    }
