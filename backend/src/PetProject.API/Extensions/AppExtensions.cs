﻿using Microsoft.EntityFrameworkCore;
using PetProject.Infrastructure;

namespace PetProject.API.Extensions;

public static class AppExtensions
{
    public static async Task<WebApplication> ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        return app;
    }
}