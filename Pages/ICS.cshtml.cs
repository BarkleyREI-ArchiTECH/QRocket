using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace QRBuddy.Pages
{
    public class IcsModel : PageModel
    {
        private readonly ILogger<IcsModel> _logger;

        public IcsModel(ILogger<IcsModel> logger) => _logger = logger;

        public void OnGet() { }
    }
}