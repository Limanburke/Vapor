using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VaporDomain.Model;
using VaporInfrastructure;


namespace VaporInfrastructure.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new VaporContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<VaporContext>>()))
        {
            if (context.Statuses.Any())
            {
                return;
            }

            context.Statuses.AddRange(
                new Status { Name = "В кошику" },
                new Status { Name = "Оплачено" },
                new Status { Name = "Скасовано" }
            );

            context.SaveChanges();
        }
    }
}

