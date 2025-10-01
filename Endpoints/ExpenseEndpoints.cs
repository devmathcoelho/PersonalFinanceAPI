using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Dtos;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Endpoints
{
    public static class ExpenseEndpoints
    {
        public static RouteGroupBuilder MapExpenseEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/expense");

            group.MapPost("/", async (AppDbContext db, Expense expense) =>
            {
                var User = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == expense.UserId);

                // If a user with this name does not exist, return NotFound
                if (User is null) return Results.NotFound();

                var Expense = new Expense
                {
                    Name = expense.Name,
                    Value = expense.Value,
                    Category = expense.Category,
                    UserId = expense.UserId
                };

                if (expense.Category == "Income")
                {
                    User.TotalRevenue += expense.Value;
                } else
                {
                    User.TotalExpense += expense.Value;
                }

                db.Expense.Add(Expense);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapGet("/{Name}", async (AppDbContext db, string Name) =>
            {
                var user = await db.Expense
                    .Where(e => e.Name == Name)
                    .ToListAsync();

                if (user is null) return Results.NotFound();

                var dto = user.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Expenses = u.User.Expenses.Select(e => new ExpenseDto
                    {
                        Name = e.Name,
                        Value = e.Value,
                        Category = e.Category
                    }).ToList(),
                });

                return Results.Ok(dto);
            });

            group.MapDelete("/{id}", async (AppDbContext db, int id) =>
            {
                var expense = await db.Expense
                    .Include(e => e.User)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (expense is null) return Results.NotFound();


                if (expense.Category == "Income")
                {
                    expense.User.TotalRevenue -= expense.Value;
                }
                else
                {
                    expense.User.TotalExpense -= expense.Value;
                }

                db.Expense.Remove(expense);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            return group;
        }
    }
}
