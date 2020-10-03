using ProgecDo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ProgecDo
{
    [DependsOn(
        typeof(ProgecDoEntityFrameworkCoreTestModule)
        )]
    public class ProgecDoDomainTestModule : AbpModule
    {

    }
}