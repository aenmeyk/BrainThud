using System.Web.Mvc;
using System.IdentityModel.Services;

namespace BrainThud.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            var host = Hosts.BRAINTHUD;
#if DEBUG
            host = Hosts.LOCALHOST;
#endif

            ViewBag.MetaDataScript = string.Format("https://brainthud.accesscontrol.windows.net/v2/metadata/identityProviders.js?protocol=wsfederation&realm={0}&version=1.0&callback=ShowSigninPage", host);
            return View("~/Views/Account/Login.cshtml");
        }

        public ActionResult SignOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return RedirectToAction("Login");
        }
    }
}
