using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParserPlanBastard.Data;
using ParserPlanBastard.Models.Entities;

namespace ParserPlanBastard.Pages
{
    public class BecomeAnAdminModel : PageModel
    {
        private readonly UserManager<Models.Entities.User> _userManager;
        private readonly ApplicationDbContext _context;

        public BecomeAnAdminModel(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [FromRoute]
        [BindProperty(SupportsGet = true)]
        public string ButtonText { get; set; }
        public async Task OnPost(string buttonText)
        {

            var user = await _userManager.GetUserAsync(User);


            if (buttonText == "A1234")
            {
                var newUserClaim = new Microsoft.AspNetCore.Identity.IdentityUserClaim<int>
                {
                    UserId = user.Id,
                    ClaimType = "AdminPassNumber",
                    ClaimValue = "A1234"
                };
                _context.UserClaims.Add(newUserClaim);
                await _context.SaveChangesAsync();

            }

        }
    }
}
