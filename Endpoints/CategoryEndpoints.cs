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

                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapDelete("/{category.Category}/{category.UserId}", async (AppDbContext db, [FromBody] Expense category) =>
            {
                var user = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == category.UserId);

                var _category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Category);

                if(user is null || _category is null) return Results.NotFound();

                db.Categories.Remove(_category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            return group;
        }
    }
}
