using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GW.Site.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your blog description page.";
        }
    }
}
