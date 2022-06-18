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
              new Product { Id = 1, Name = "Men jeans ", color = "blue", Price = 14.99, ProductImage = "https://sultan.blob.core.windows.net/attac/jeans.png", size = "34", ProductCategoryId = 1 },
              new Product { Id = 15, Name = "Men jeans shirt", color = "jeans", Price = 11.99, ProductImage = "https://sultan.blob.core.windows.net/attac/jeansShirt.png", size = "L", ProductCategoryId = 1 },
              new Product { Id = 16, Name = "Men Suit", color = "vinous", Price = 99.99, ProductImage = "https://sultan.blob.core.windows.net/attac/MenSuits.png", size = "52", ProductCategoryId = 1 },


              new Product { Id = 17, Name = "Women jeans ", color = "blue", Price = 15.99, ProductImage = "https://sultan.blob.core.windows.net/attac/Wjens.png", size = "28", ProductCategoryId = 2 },
              new Product { Id = 18, Name = "Women White Stripe Longline Blouse ", color = "white", Price = 13.99, ProductImage = "https://sultan.blob.core.windows.net/attac/WhiteBlouse.png", size = "M", ProductCategoryId = 2 },
              new Product { Id = 2, Name = "Women's T-Shirts", color = "Pink", Price = 12.99, ProductImage = "https://sultan.blob.core.windows.net/attac/WomenT-Shirts.png", size = "L", ProductCategoryId = 2 },
             
              new Product { Id = 3, Name = "Fleece Bear", color = "white", Price = 14.99,ProductImage = "https://sultan.blob.core.windows.net/attac/FleeceBear.png", size = "s", ProductCategoryId = 3 },
              new Product { Id = 19, Name = "Baby Jacket", color = "Pink", Price = 19.99, ProductImage = "https://sultan.blob.core.windows.net/attac/BabyJacket.png", size = "S", ProductCategoryId = 3 },
              new Product { Id = 20, Name = "baby set", color = "jeans", Price = 18.99, ProductImage = "https://sultan.blob.core.windows.net/attac/BabyJeans.png", size = "L", ProductCategoryId = 3 },

              new Product { Id = 4, Name = "Men Suit", color = "vinous", Price = 99.99, ProductImage = "https://sultan.blob.core.windows.net/attac/MenSuits.png", size = "52", ProductCategoryId = 4 },
              new Product { Id = 30, Name = "Fleece Bear", color = "white", Price = 14.99, ProductImage = "https://sultan.blob.core.windows.net/attac/FleeceBear.png", size = "s", ProductCategoryId = 4 },
              new Product { Id = 31, Name = "Men SPORTS WEAR", color = "Black", ProductImage = "https://sultan.blob.core.windows.net/attac/MenSPORTSWEAR.png", Price = 35.99, size = "L", ProductCategoryId = 4 },

              new Product { Id = 5, Name = "white Bag", color = "white", Price = 12.99, ProductImage = "https://sultan.blob.core.windows.net/attac/whiteBAGS.png", size = "N/A", ProductCategoryId = 5 },
              new Product { Id = 21, Name = "BackPack", color = "Black", Price = 18.99, ProductImage = "https://sultan.blob.core.windows.net/attac/BlackBag.png", size = "N/A", ProductCategoryId = 5 },
              new Product { Id = 22, Name = "Shoulder Bag ", color = "offwhite", Price = 8.99, ProductImage = "https://sultan.blob.core.windows.net/attac/shoulderbag.png", size = "N/A", ProductCategoryId = 5 },


              new Product { Id = 6, Name = "Polo shirt", color = "Pink", Price = 11.99, ProductImage = "https://sultan.blob.core.windows.net/attac/poloB.png", size = "L", ProductCategoryId = 6 },
              new Product { Id = 23, Name = "Boy Jacket", color = "Green and white", Price = 28.99, ProductImage = "https://sultan.blob.core.windows.net/attac/boyj.png", size = "M", ProductCategoryId = 6 },
              new Product { Id = 24, Name = "Boy Suit", color = "Navy blue", Price = 65.99, ProductImage = "https://sultan.blob.core.windows.net/attac/BoyS.png", size = "42", ProductCategoryId = 6 },

              new Product { Id = 7, Name = "Fleece Bear", color = "white", Price = 14.99, ProductImage = "https://sultan.blob.core.windows.net/attac/FleeceBear.png", size = "s", ProductCategoryId = 8 },
              new Product { Id = 25, Name = "Boy Suit", color = "Navy blue", Price = 65.99, ProductImage = "https://sultan.blob.core.windows.net/attac/BoyS.png", size = "42", ProductCategoryId = 8 },
              new Product { Id = 26, Name = "Dress ", color = "blue", Price = 15.99, ProductImage = "https://sultan.blob.core.windows.net/attac/GirlsDress.png", size = "34", ProductCategoryId = 8 },


              new Product { Id = 8, Name = "Dress ", color = "blue", Price = 15.99, ProductImage = "https://sultan.blob.core.windows.net/attac/GirlsDress.png", size = "M", ProductCategoryId = 7 },
              new Product { Id = 27, Name = "Girl shirt", color = "White and blue", Price = 16.99, ProductImage = "https://sultan.blob.core.windows.net/attac/girlsB.png", size = "L", ProductCategoryId = 7 },
              new Product { Id = 29, Name = "Girl Jacket", color = "Black and white", Price = 29.99, ProductImage = "https://sultan.blob.core.windows.net/attac/GilrsJ.png", size = "S", ProductCategoryId = 7 },

              new Product { Id = 9, Name = "Men Shoes", color = "white", ProductImage = "https://sultan.blob.core.windows.net/attac/shos.png", Price = 12.99, size = "41", ProductCategoryId = 9 },
              new Product { Id = 10, Name = "Women Shoes", color = "Pink", ProductImage = "https://sultan.blob.core.windows.net/attac/WomenShoes.png", Price = 12.99, size = "37", ProductCategoryId = 9 },
              new Product { Id = 11, Name = "Child Sport Shoes", color = "white", ProductImage = "https://sultan.blob.core.windows.net/attac/ChildSportShoes.png", Price = 12.99, size = "20", ProductCategoryId = 9 },

              new Product { Id = 12, Name = "Men SPORTS WEAR", color = "Black", ProductImage = "https://sultan.blob.core.windows.net/attac/MenSPORTSWEAR.png", Price = 35.99, size = "L", ProductCategoryId = 10 },
              new Product { Id = 13, Name = "Women SPORTS WEAR", color = "Selver", ProductImage = "https://sultan.blob.core.windows.net/attac/WomenTrackSuits.jfif", Price = 32.99, size = "M", ProductCategoryId = 10 },
              new Product { Id = 14, Name = "Child SPORTS WEAR", color = "vinous", ProductImage = "https://sultan.blob.core.windows.net/attac/KidsSPORTSWEAR.png", Price = 18.99, size = "S", ProductCategoryId = 10 }

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
