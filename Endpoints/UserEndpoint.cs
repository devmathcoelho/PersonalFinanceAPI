using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceAPI.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        group.MapGet("/", async (AppDbContext db) =>
            await db.Users.ToListAsync());

        group.MapGet("/random", async (AppDbContext db) =>
            await db.Users
                .OrderBy(u => Guid.NewGuid())
                .Take(5)
                .ToListAsync());

        group.MapPost("/", async (AppDbContext db, User user) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        return group;
    }
}
