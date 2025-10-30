using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Dtos;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

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
                Bills = u.Bills.Select(b => new BillDto
                {
                    Name = b.Name,
                    Value = b.Value,
                    DueDate = b.DueDate,
                    IsPaid = b.IsPaid
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
            .FirstOrDefaultAsync(u => u.Name == name);

            if (user is null) return Results.NotFound();

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                TotalRevenue = user.TotalRevenue,
                TotalExpense = user.TotalExpense,
                Categories = user.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Value = c.Value,
                    Month = c.Month
                }).ToList(),
                Expenses = user.Expenses.Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Value = e.Value,
                    Category = e.Category,
                    Date = e.Date,
                    UserId = e.UserId
                }).ToList(),
                Bills = user.Bills.Select(b => new BillDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Value = b.Value,
                    DueDate = b.DueDate,
                    IsPaid = b.IsPaid
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

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        group.MapDelete("/{name}", async (AppDbContext db, string name) =>
        {
            // Gets the user with the specified name
            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Name == name);

            if (user is null) return Results.NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.Ok();
        });

        return group;
    }
}
