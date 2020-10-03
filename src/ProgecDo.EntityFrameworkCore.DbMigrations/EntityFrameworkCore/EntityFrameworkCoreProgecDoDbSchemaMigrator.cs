using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgecDo.Data;
using Volo.Abp.DependencyInjection;

namespace ProgecDo.EntityFrameworkCore
{
    public class EntityFrameworkCoreProgecDoDbSchemaMigrator
        : IProgecDoDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreProgecDoDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ProgecDoMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ProgecDoMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}