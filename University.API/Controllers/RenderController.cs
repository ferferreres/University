using System.Web.Mvc;

namespace University.API.Controllers
{
    public class RenderController : Controller
    {
        [HttpGet()]
        // GET: Render
        public ActionResult GetHtml(string view)
        {
            return View(view);
        }
    }
}