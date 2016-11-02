using System.Web.Mvc;

namespace PhotoManagerThreeLayer.Controllers
{
    public class PhotoController : Controller
    {


        // GET: /Photo/Details/5
        public ActionResult Details(int id = 0)
        {

            return RedirectToAction("Index", "Album", new { area = "" });
        }

        // GET: /Photo/Edit/5

        public ActionResult Edit(int id = 0)
        {

            return RedirectToAction("Index", "Album", new { area = "" });
        }

        // GET: /Photo/Delete/5

        public ActionResult Delete(int id = 0)
        {

            return RedirectToAction("Index", "Album", new { area = "" });
        }

        // GET: /Photo/Create
        public ActionResult Create(int albumId)
        {
            int id = albumId;
            return RedirectToAction("Index", "Album", new { area = "" });
        }
    }
}