using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<User>().HasData(
                new User 
                {
                    Id = "1023",
                    Email = "zeyneb@gmail.com",
                    FirstName = "Zeyneb",
                    LastName = "Hesenzade",
                    Address = "Azərbaycan",
                    PhoneNumber = "+994-55-5454452", 
                    UserName = "zeyneb@gmail.com", 
                    ConcurrencyStamp = "sdchsuicmkms",
                    EmailConfirmed = true,
                    NormalizedEmail = "ZEYNEB@GMAIL.COM",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    NormalizedUserName = "ZEYNEB@GMAIL.COM",
                    TwoFactorEnabled = true,
                    AccessFailedCount = 1,
                    LockoutEnd = DateTime.Now,
                    PasswordHash = null,
                    SecurityStamp = null,
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "abs21",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "sdxjwdxjmskm"
                }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "abs21",
                    UserId = "1023"
                });
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
