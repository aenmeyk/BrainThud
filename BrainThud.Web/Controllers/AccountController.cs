using System.Net;
using System.Web.Mvc;
using BrainThud.Core;
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
            var host = Urls.BRAINTHUD;
#if DEBUG
            host = Urls.LOCALHOST;
#endif

            host = WebUtility.UrlEncode(host);
            ViewBag.MetaDataScript = string.Format(Urls.IDENTITY_PROVIDERS, host);
            return View("Login");
        }

        public ActionResult SignOut()
        {
            var signOutUrl = this.authenticationHelper.SignOut();
            return new RedirectResult(signOutUrl);
        }
    }
}
