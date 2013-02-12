using System.Web.Mvc;
using BrainThud.Web.Authentication;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AccountController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var host = Hosts.BRAINTHUD;
#if DEBUG
            host = Hosts.LOCALHOST;
#endif

            ViewBag.MetaDataScript = string.Format("https://brainthud.accesscontrol.windows.net/v2/metadata/identityProviders.js?protocol=wsfederation&realm={0}&version=1.0&callback=ShowSigninPage", host);
            return View("Login");
        }

        public ActionResult SignOut()
        {
            this.authenticationHelper.SignOut();
            return RedirectToAction("Login");
        }
    }
}
