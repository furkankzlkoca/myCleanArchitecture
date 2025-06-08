using Microsoft.EntityFrameworkCore;
using myCleanArchitecture.Infrastructure.Identity;

namespace myCleanArchitecture.Infrastructure.Context
{
    public class ApplicationDbInitializer
    {
        public static void SeedData(ModelBuilder _modelBuilder)
        {
            AppRole adminRole = new()
            {
                Id = Guid.Parse("A7B2C3D4-E5F6-7890-1234-567890ABCDEF"),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "FC7F9F46-E80D-435A-998A-09AA28350CDD"
            };
            _modelBuilder.Entity<AppRole>().HasData(adminRole);

            AppUser appUser = new()
            {
                Id = Guid.Parse("DC142E5C-FC4F-43A2-9D44-34A1FB3D5A7A"),
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@ADMIN.COM", // Hardcoded uppercase
                NormalizedUserName = "ADMIN", // Hardcoded uppercase
                // Admin12345**
                PasswordHash = "AQAAAAIAAYagAAAAENlWt3Ve0xytdz7xBD4j3eRZ0gV0qVnPqj7bYd/Ah2VtWtFpZ5DkXWklq6Gk3wR1Bg==", // Precomputed hash 
                ConcurrencyStamp = "1A2B3C4D-5E6F-7890-1234-567890ABCDEF" // Add fixed concurrency stamp
            };
            _modelBuilder.Entity<AppUser>().HasData(appUser);

            AppUserRole userRole = new()
            {
                UserId = appUser.Id,
                RoleId = adminRole.Id
            };
            _modelBuilder.Entity<AppUserRole>().HasData(userRole);


            _modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.Parse("F96E3FD7-E977-47BC-B915-F8FDAB4981EB"), IsActive = true, Name = "Electronics", CreatedBy = appUser.Id.ToString(), Created = Convert.ToDateTime("2025-06-04 16:29:17.730") },
                new Category { Id = Guid.Parse("85C86B71-6777-46F3-88AF-EAC6F0082E1E"), IsActive = true, Name = "Books", CreatedBy = appUser.Id.ToString(), Created = Convert.ToDateTime("2025-06-04 16:29:17.730") }
            );
            _modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("99B8F183-6CA5-4347-90B0-A6F30957902F"), Price = 999.9m, StockQuantity = 150, Name = "Laptop", CategoryId = Guid.Parse("F96E3FD7-E977-47BC-B915-F8FDAB4981EB"), CreatedBy = appUser.Id.ToString(), Created = Convert.ToDateTime("2025-06-04 16:29:17.730") },
                new Product { Id = Guid.Parse("ED3B1792-7654-4F96-BC53-6E5397413AC0"), Price = 1200, StockQuantity = 200, Name = "Smartphone", CategoryId = Guid.Parse("F96E3FD7-E977-47BC-B915-F8FDAB4981EB"), CreatedBy = appUser.Id.ToString(), Created = Convert.ToDateTime("2025-06-04 16:29:17.730") },
                new Product { Id = Guid.Parse("329FEFE4-B1A0-43A9-9E0C-3CEA070E1D65"), Price = 25, StockQuantity = 225, Name = "C# Programming Book", CategoryId = Guid.Parse("85C86B71-6777-46F3-88AF-EAC6F0082E1E"), CreatedBy = appUser.Id.ToString(), Created = Convert.ToDateTime("2025-06-04 16:29:17.730") }
            );
        }
    }
}
