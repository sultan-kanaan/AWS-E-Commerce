using AWS_E_Commerce.Auth.Models;
using AWS_E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Models.DTOs;

namespace AWS_E_Commerce.Data
{
    public class AWSDbContext : IdentityDbContext<ApplicationUser>
    {
        public AWSDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductCategory>().HasData(
             new ProductCategory { Id = 1, Name = "MEN" },
             new ProductCategory { Id = 2, Name = "WOMEN" },
             new ProductCategory { Id = 3, Name = "BABY" },
             new ProductCategory { Id = 4, Name = "NEW ARRIVALS" },
             new ProductCategory { Id = 5, Name = "BAGS" },
             new ProductCategory { Id = 6, Name = "BOYS" },
             new ProductCategory { Id = 7, Name = "GIRLS" },
             new ProductCategory { Id = 8, Name = "BUY 1 GET 1 FREE" },
             new ProductCategory { Id = 9, Name = "SHOES" },
             new ProductCategory { Id = 10, Name = "SPORTSWEAR" }


           );
            modelBuilder.Entity<Product>().HasData(
              new Product { Id = 1, Name = "jens ", color = "blue", Price = 15, size = "34", ProductCategoryId = 1 },
              new Product { Id = 2, Name = "shirt", color = "red", Price = 12, size = "L", ProductCategoryId = 2 },
              new Product { Id = 3, Name = "babyshirt", color = "white", Price = 8, size = "s", ProductCategoryId = 3 },
              new Product { Id = 4, Name = "jens ", color = "blue", Price = 15, size = "34", ProductCategoryId = 4 },
              new Product { Id = 5, Name = "shirt", color = "red", Price = 12, size = "L", ProductCategoryId = 5 },
              new Product { Id = 6, Name = "shirt", color = "red", Price = 12, size = "L", ProductCategoryId = 6 },
              new Product { Id = 7, Name = "jens ", color = "blue", Price = 15, size = "34", ProductCategoryId = 7 },
              new Product { Id = 8, Name = "shirt", color = "red", Price = 12, size = "L", ProductCategoryId = 8 },
              new Product { Id = 9, Name = "shirt", color = "red", Price = 12, size = "L", ProductCategoryId = 9 }
            );


            SeedRole(modelBuilder, "Administrator", "Administrator", "Editor");
            SeedRole(modelBuilder, "Editor", "Editor");
            SeedRole(modelBuilder, "Customer", "Customer");


        }

        private int nextId = 1; // we need this to generate a unique id on our own
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions", // This matches what we did in Startup.cs
                ClaimValue = permission
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }

}
