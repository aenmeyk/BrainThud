using System.Net;
using System.Web.Mvc;
using BrainThud.Web.Authentication;

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

            host = WebUtility.UrlEncode(host);
            ViewBag.MetaDataScript = string.Format(@"https://brainthud.accesscontrol.windows.net/v2/metadata/IdentityProviders.js?protocol=wsfederation&realm={0}&reply_to=&context=rm%3d0%26id%3dpassive%26ru%3d%252f&request_id=&version=1.0&callback=ShowSigninPage", host);
            return View("Login");
        }

        public ActionResult SignOut()
        {
            this.authenticationHelper.SignOut();
            return RedirectToAction("Login");
        }
    }
}
