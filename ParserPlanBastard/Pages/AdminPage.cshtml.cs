using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ParserPlanBastard.Pages
{
    [Authorize("StatusAdmin")]
    public class AdminPageModel : PageModel
    {
        public void OnGet()
        {
        }

        public RedirectToPageResult OnPostTest()
        {
            return RedirectToPage("Privacy");
        }
    }
}
