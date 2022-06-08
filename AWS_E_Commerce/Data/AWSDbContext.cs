using AWS_E_Commerce.Auth.Models;
using AWS_E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Data
{
    public class AWSDbContext : IdentityDbContext<ApplicationUser>
    {
        public AWSDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductCategory>().HasData(
             new ProductCategory { Id = 1, Name = "Mens " },
             new ProductCategory { Id = 2, Name = "Womens" }
           );
            modelBuilder.Entity<Product>().HasData(
              new Product { Id = 1, Name = "jens ", color = "blue", Price = 15, size = "34", description = "test", ProductCategoryId = 1 },
              new Product { Id = 2, Name = "shirt", color = "red", Price = 12, size = "L", description = "test", ProductCategoryId = 2 }
            );


            SeedRole(modelBuilder, "Administrator", "Administrator", "Editor");
            SeedRole(modelBuilder, "Editor", "Editor");

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
