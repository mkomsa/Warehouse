using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Warehouse.Infrastructure.DAL;

namespace Warehouse.Infrastructure.Seeder;

public class AppDbContextSeeder(AppDbContext dbContext)
{
    public async void SeedDatabase()
    {
        string[] seedFiles = { "Addresses.sql", "Customers.sql", "Invoices.sql", "Manufacturers.sql", "Orders.sql", "ParcelsInfo.sql", "Products.sql", "OrdersProduct.sql" };
        List<string> sqlCommands = new();

        foreach (var seedFile in seedFiles)
        {
            Assembly? assembly = Assembly.GetAssembly(typeof(AppDbContextSeeder));
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
            string filePath = seedFile;

            if (!seedFile.StartsWith(nameof(Infrastructure)))
            {
                filePath = assembly
                    .GetManifestResourceNames()
                    .Single(str => str.EndsWith(seedFile));
            }

            await using Stream? stream = assembly.GetManifestResourceStream(filePath);
            using StreamReader reader = new StreamReader(stream);

            string fileContent = await reader.ReadToEndAsync(CancellationToken.None);
            List<string> commands = fileContent.Split(";").ToList();

            sqlCommands.AddRange(commands);
        }

        if (!dbContext.Addresses.Any())
        {
            await using IDbContextTransaction transaction =
                await dbContext.Database.BeginTransactionAsync(CancellationToken.None);

            foreach (string sqlCommand in sqlCommands)
            {
                if (!string.IsNullOrWhiteSpace(sqlCommand))
                {
                    dbContext.Database.ExecuteSqlRaw(sqlCommand);
                }
            }

            await dbContext.SaveChangesAsync(CancellationToken.None);
            await transaction.CommitAsync(CancellationToken.None);
        }
    }
}
