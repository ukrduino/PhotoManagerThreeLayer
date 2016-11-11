using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using PhotoManager.BLL.Services;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.ViewModels;

namespace PhotoManagerThreeLayer.Controllers
{
    public class PhotoController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BllCategoryServices _categoryServices = new BllCategoryServices();
        //private BllCommentServices _commentServices = new BllCommentServices();

        public ActionResult Index()
        {
            IEnumerable<PhotoDTO> photoDtoList = _photoServices.GetAllPhotos(); //TODO server side pagination or limit number of albums or by user (NOT ALL)
            return View(Mapper.Map<IEnumerable<PhotoDTO>, IEnumerable<PhotoListViewModel>>(photoDtoList));
        }

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