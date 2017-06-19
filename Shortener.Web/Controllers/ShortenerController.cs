using System.Web.Mvc;
using TestTask.Shortener.BL;

namespace Shortener.Web.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IShortenerBL ShortenerLogic;
        public ShortenerController(IShortenerBL shortenerLogic)
        {
            ShortenerLogic = shortenerLogic;
        }

        public ActionResult Shorten(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var original = ShortenerLogic.GetOriginalLink(id);
                if (!string.IsNullOrEmpty(original))
                {
                    ShortenerLogic.ClickInc(id);
                    return Redirect(original);
                }
            }
            return View();
        }
    }

}