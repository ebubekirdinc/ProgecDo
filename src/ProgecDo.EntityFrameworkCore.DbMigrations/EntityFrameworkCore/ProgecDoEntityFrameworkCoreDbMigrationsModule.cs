using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ProgecDo.EntityFrameworkCore
{
    [DependsOn(
        typeof(ProgecDoEntityFrameworkCoreModule)
        )]
    public class ProgecDoEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProgecDoMigrationsDbContext>();
        }
    }
}
