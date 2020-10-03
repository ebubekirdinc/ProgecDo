using ProgecDo.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ProgecDo.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class ProgecDoPageModel : AbpPageModel
    {
        protected ProgecDoPageModel()
        {
            LocalizationResourceType = typeof(ProgecDoResource);
        }
    }
}