using Volo.Abp.Modularity;

namespace ProgecDo
{
    [DependsOn(
        typeof(ProgecDoApplicationModule),
        typeof(ProgecDoDomainTestModule)
        )]
    public class ProgecDoApplicationTestModule : AbpModule
    {

    }
}