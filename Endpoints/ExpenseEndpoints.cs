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

                var Expense = new Expense
                {
                    Name = expense.Name,
                    Value = expense.Value,
                    Category = expense.Category,
                    UserId = expense.UserId
                };
                
                db.Expense.Add(Expense);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            return group;
        }
    }
}
