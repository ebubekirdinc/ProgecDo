using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProgecDo.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ProgecDoMigrationsDbContextFactory : IDesignTimeDbContextFactory<ProgecDoMigrationsDbContext>
    {
        public ProgecDoMigrationsDbContext CreateDbContext(string[] args)
        {
            ProgecDoEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ProgecDoMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ProgecDoMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProgecDo.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
