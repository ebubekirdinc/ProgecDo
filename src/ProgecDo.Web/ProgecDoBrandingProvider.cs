using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace ProgecDo.Web
{
    [Dependency(ReplaceServices = true)]
    public class ProgecDoBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "ProgecDo";
    }
}
