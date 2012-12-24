using System.Web.Mvc;

namespace BrainThud.Web.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return this.View("SpecRunner");
        }
    }
}
