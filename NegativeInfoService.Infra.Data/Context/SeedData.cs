using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NegativeInfoService.Domain.Entities;
using System;

namespace NegativeInfoService.Infra.Data.Context
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Exit if DB has been seeded
                if (await context.Negations.AnyAsync())
                    return;

                context.Negations.AddRange(
                    new Negation(
                        1,
                        71903286026,
                        "ITAU-3458-098-9872",
                        DateTime.Parse("2021-01-09"),
                        1000.10f),
                    new Negation(
                        2,
                        11493925091,
                        "ITAU-3458-098-9872",
                        DateTime.Parse("2010-05-17"),
                        5687.31f)
                );

                context.SaveChanges();
            }
        }
    }
}
