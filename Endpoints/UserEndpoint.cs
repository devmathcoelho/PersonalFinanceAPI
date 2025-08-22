using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Dtos;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/user");

        group.MapGet("/", async (AppDbContext db) =>
        {
            // Gets all users
            var dto = db.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Categories = u.Categories.Select(c => new CategoryDto
                {
                    Name = c.Name
                }).ToList(),
                Expenses = u.Expenses.Select(e => new ExpenseDto
                {
                    Name = e.Name,
                    Value = e.Value,
                    Category = e.Category
                }).ToList(),
                Revenues = u.Revenues.Select(i => new RevenueDto
                {
                    Name = i.Name,
                    Value = i.Value,
                }).ToList()
            });

            if (!dto.Any()) return Results.NotFound();

            return Results.Ok(dto);
        });

        group.MapGet("/{name}", async (AppDbContext db, string name) =>
        {
            // Gets the user with the specified name
            // Including their categories, expenses, and revenues
            var user = await db.Users
            .Include(u => u.Categories)
            .Include(u => u.Expenses)
            .Include(u => u.Revenues)
            .FirstOrDefaultAsync(u => u.Name == name);

            if (user is null) return Results.NotFound();

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Categories = user.Categories.Select(c => new CategoryDto
                {
                    Name = c.Name
                }).ToList(),
                Expenses = user.Expenses.Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Value = e.Value,
                    Category = e.Category
                }).ToList(),
                Revenues = user.Revenues.Select(i => new RevenueDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Value = i.Value,
                }).ToList()
            };

            return Results.Ok(dto);
        });
        group.MapPost("/", async (AppDbContext db, User user) =>
        {
            var userExists = await db.Users
                .FirstOrDefaultAsync(u => u.Name == user.Name);

            // If a user with the same name already exists, return BadRequest
            if (userExists is not null) return Results.BadRequest();

            if ((user.CreatedAt is null) || (user.CreatedAt == ""))
            {
                user.CreatedAt = DateTime.Now.ToString();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        return group;
    }
}
