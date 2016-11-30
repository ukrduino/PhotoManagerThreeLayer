using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.BLL.Services;
using PhotoManager.BLL.Utils;
using PhotoManagerThreeLayer.ViewModels;

namespace PhotoManagerThreeLayer.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BLLImageServices _imageServices = new BLLImageServices();
        private BllCommentServices _commentServices = new BllCommentServices();

        public ActionResult Index()
        {
            IEnumerable<PhotoDTO> photoDtoList = _photoServices.GetAllPhotos(); //TODO server side pagination or limit number of albums or by user (NOT ALL)
            return View(Mapper.Map<IEnumerable<PhotoDTO>, IEnumerable<PhotoListViewModel>>(photoDtoList));
        }

        // GET: /Photo/Details/5
        public ActionResult Details(int id = 0)
        {
            PhotoDTO photoDto = _photoServices.GetPhoto(id);
            if (photoDto == null)
            {
                return HttpNotFound();
            }
            PhotoDetailViewModel photoDetailViewModel = Mapper.Map<PhotoDetailViewModel>(photoDto);
            List<PhotoCommentDTO> photoCommentsDto = _commentServices.GetCommentsByPhoto(id);
            photoDetailViewModel.Comments = Mapper.Map<List<PhotoCommentDTO>, List<PhotoCommentViewModel>>(photoCommentsDto);
            return View(photoDetailViewModel);
        }

        // GET: /Photo/Delete/5
        public ActionResult Delete(int id = 0)
        {

            return RedirectToAction("Index", "Album", new { area = "" });
        }

        // GET: /Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Photo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoDetailViewModel photoDetailViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                PhotoDTO photoDto = Mapper.Map<PhotoDTO>(photoDetailViewModel);
                byte[] imageData = null;
                if (file != null)
                {
                    if (file.ContentLength > (500 * 1024))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File size must be less than 500 Kb");
                        return View(photoDetailViewModel);
                    }
                    if (!file.IsJpgImage())
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File type allowed : jpeg");
                        return View(photoDetailViewModel);
                    }
                    photoDto.ImageId = _imageServices.SaveImageToDb(file.InputStream, Enums.ImageSize.Small);
                    photoDto.Created = DateTime.Now; ;
                    photoDto.AnyOneCanSee = true;
                }
                _photoServices.CreatePhoto(photoDto);
                return RedirectToAction("Index");
            }
            return View(photoDetailViewModel);
        }

        // GET: /Album/Edit/5
        public ActionResult Edit(int id = 0)
        {
            PhotoDTO photoDto = _photoServices.GetPhoto(id);
            if (photoDto == null)
            {
                return HttpNotFound();
            }
            PhotoDetailViewModel photoDetailViewModel = Mapper.Map<PhotoDetailViewModel>(photoDto);
            return View(photoDetailViewModel);
        }

        // POST: /Photo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhotoDetailViewModel photoDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                PhotoDTO photoDto = Mapper.Map<PhotoDTO>(photoDetailViewModel);
                _photoServices.UpdatePhoto(photoDto);
                return RedirectToAction("Details", new { id = photoDetailViewModel.Id });
            }
            return View(photoDetailViewModel);
        }

        //TODO needs refactoring, album should contain not image but link on photo from this album
        public ActionResult GetPhotoCoverImage(int id)
        {
            return File(_imageServices.GetImageBytesFromDb(id), "image/jpg");
        }
    }
}