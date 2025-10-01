using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Dtos;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/category");
            
            group.MapPost("/", async (AppDbContext db, [FromBody] Category category) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == category.UserId);

                // If a user with this name does not exist, return NotFound
                if (User is null) return Results.NotFound();

                var Category = new Category
                {
                    Name = category.Name,
                    UserId = category.UserId
                };

                db.Categories.Add(Category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapDelete("/{category.Name}/{category.UserId}", async (AppDbContext db, [FromBody] CategoryDto category) =>
            {
                var user = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == category.UserId);

                var _category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name);

                if (user is null || _category is null) return Results.NotFound();
                if (_category is null) return Results.NotFound();

                db.Categories.Remove(_category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            return group;
        }
    }
}
