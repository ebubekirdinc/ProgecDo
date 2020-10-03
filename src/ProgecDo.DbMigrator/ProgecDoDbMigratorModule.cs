using ProgecDo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ProgecDo.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ProgecDoEntityFrameworkCoreDbMigrationsModule),
        typeof(ProgecDoApplicationContractsModule)
        )]
    public class ProgecDoDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
