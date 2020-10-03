using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace ProgecDo.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(ProgecDoHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ProgecDoConsoleApiClientModule : AbpModule
    {
        
    }
}
