using Microsoft.AspNetCore.Mvc;

namespace ProgecDo.Web.Pages
{
    public class IndexModel : ProgecDoPageModel
    {
        public void OnGet()
        {
            
        }
        
        public IActionResult OnGetProjectsViewComponent()
        {
            return ViewComponent("Projects");
        }
    }
}