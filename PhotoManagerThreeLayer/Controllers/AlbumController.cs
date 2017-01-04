using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using log4net;
using PhotoManager.BLL.DTOModels;
using PhotoManager.BLL.Services;
using PhotoManagerThreeLayer.ViewModels;

namespace PhotoManagerThreeLayer.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BLLImageServices _imageServices = new BLLImageServices();
        private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            IEnumerable<AlbumDTO> albumDtoList = _albumServices.GetAllAlbums(); //TODO server side pagination or limit number of albums or by user (NOT ALL)
            logger.Debug("Hello logger");
            return View(Mapper.Map<IEnumerable<AlbumDTO>, IEnumerable<AlbumListViewModel>>(albumDtoList));
        }

        // GET: /Album/Manage/5
        public ActionResult Manage(int id = 0)
        {
            AlbumDTO albumDto = _albumServices.GetAlbum(id);
            if (albumDto == null)
            {
                return HttpNotFound();
            }
            AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
            return View(albumDetailViewModel);
        }

        // GET: /Album/Details/5
        public ActionResult Details(int id = 0)
        {
            AlbumDTO albumDto = _albumServices.GetAlbum(id);
            if (albumDto == null)
            {
                return HttpNotFound();
            }
            AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
            return View(albumDetailViewModel);
        }

        // GET: /Album/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Album/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumDetailViewModel viewAlbum)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurityService.IsPayedUser() &&
                    BllAlbumServices.GetAlbumsNumberForCurrentUser() > 5)
                {
                    ModelState.AddModelError("NumberOfAlbumsValidationError", "Free users can have only 5 albums (please upgrade to payed user)");
                    return View(viewAlbum);
                }
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(viewAlbum);
                albumDto.Created = DateTime.Now;
                _albumServices.CreateAlbum(albumDto);
                return RedirectToAction("Index");
            }
            return View(viewAlbum);
        }

        // GET: /Album/Edit/5
        public ActionResult Edit(int id = 0)
        {
            AlbumDTO albumDto = _albumServices.GetAlbum(id);
            if (albumDto == null)
            {
                return HttpNotFound();
            }
            AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
            return View(albumDetailViewModel);
        }

        // POST: /Album/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumDetailViewModel albumDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(albumDetailViewModel);
               _albumServices.UpdateAlbum(albumDto);
                return RedirectToAction("Manage", new { id = albumDetailViewModel.Id });
            }
            return View(albumDetailViewModel);
        }

        // POST: /Album/RemovePhotoFromAlbum
        [HttpPost]
        public void RemovePhotoFromAlbum(string albumId, string photoId)
        {
            _albumServices.RemovePhotoFromAlbum(int.Parse(albumId), int.Parse(photoId));
        }

        // POST: /Album/AddPhotoToAlbum
        [HttpPost]
        public void AddPhotoToAlbum(string albumId, string photoId)
        {
            _albumServices.AddPhotoToAlbum(int.Parse(albumId), int.Parse(photoId));
        }

        [AllowAnonymous]
        public ActionResult GetAlbumImage(int id)
        {
            return File(_imageServices.GetImageBytesFromDb(id), "image/jpg");
        }

        public ContentResult GetDirectAlbumLink(int id)
        {
            AlbumDTO albumDto = _albumServices.GetAlbum(id);
            string link = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + albumDto.TitleSlug;
            return Content(link);
        }

        [AllowAnonymous]
        public ActionResult DirectLinkAlbumAccess(string titleSlug)
        {
            AlbumDTO albumDto = _albumServices.GetAlbumBySlug(titleSlug);
            if (albumDto == null)
            {
                return HttpNotFound();
            }
            AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
            return View("Details", albumDetailViewModel);
        }
    }
}