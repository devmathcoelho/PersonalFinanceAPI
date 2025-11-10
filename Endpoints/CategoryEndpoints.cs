using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/category");

            group.MapGet("/{userName}", async (AppDbContext db, string userName) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Name == userName);

                // If a user with this name does not exist, return NotFound
                if (User is null) return Results.NotFound();

                var categories = await db.Categories
                    .Where(c => c.UserId == User.Id)
                    .ToListAsync();

                return Results.Ok(categories);
            });
            
            group.MapPost("/", async (AppDbContext db, [FromBody] Category category) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == category.UserId);

                // If a user with this name does not exist, return NotFound
                if (User is null) return Results.NotFound();

                var _Category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name && c.Month == category.Month);

                if (_Category is not null) return Results.Conflict();

                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapPut("/{user}/{month}/{value}/remove", async (AppDbContext db, int month, int value, string user) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Name == user);

                var _Category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Month == month);

                if (User is null || _Category is null) return Results.NotFound();

                _Category.Value -= value;

                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapPut("/{user}/{month}/{value}/add", async (AppDbContext db, int month, int value, string user) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Name == user);

                var _Category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Month == month);

                if (User is null || _Category is null) return Results.NotFound();

                _Category.Value += value;

                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapDelete("/{UserName}/{CategoryName}", async (AppDbContext db, string UserName, string CategoryName) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Name == UserName);

                if (User is null) return Results.NotFound();

                var Category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Name == CategoryName && c.UserId == User.Id);

                if (Category is null) return Results.NotFound();

                db.Categories.Remove(Category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            return group;
        }
    }
}
