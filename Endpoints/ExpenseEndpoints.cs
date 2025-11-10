using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
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

                if (expense.Category == "Income")
                {
                    User.TotalRevenue += (float)expense.Value;
                } else
                {
                    User.TotalExpense += (float)expense.Value;
                }

                if(expense.Date == "" || expense.Date == null)
                {
                    expense.Date = DateTime.UtcNow.ToString("dd-MM-yyyy");
                }

                db.Expense.Add(expense);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            group.MapDelete("/{id}", async (AppDbContext db, int id) =>
            {
                var expense = await db.Expense
                    .Include(e => e.User)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (expense is null) return Results.NotFound();


                if (expense.Category == "Income")
                {
                    expense.User.TotalRevenue -= (float)expense.Value;
                }
                else
                {
                    expense.User.TotalExpense -= (float)expense.Value;
                }

                db.Expense.Remove(expense);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            return group;
        }
    }
}
