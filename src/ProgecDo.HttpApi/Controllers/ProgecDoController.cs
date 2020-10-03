using ProgecDo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ProgecDo.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ProgecDoController : AbpController
    {
        protected ProgecDoController()
        {
            LocalizationResource = typeof(ProgecDoResource);
        }
    }
}