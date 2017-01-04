using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BLLImageServices _imageServices = new BLLImageServices();
        private BllCommentServices _commentServices = new BllCommentServices();

        public ActionResult Index()
        {
            IEnumerable<PhotoDTO> photoDtoList = _photoServices.GetAllPhotos(); //TODO server side pagination or limit number of albums or by user (NOT ALL)
            return View(Mapper.Map<IEnumerable<PhotoDTO>, IEnumerable<PhotoListViewModel>>(photoDtoList));
        }

        // GET: /Photo/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            PhotoDTO photoDto = _photoServices.GetPhoto(id);
            if (photoDto == null)
            {
                return HttpNotFound();
            }
            PhotoDetailViewModel photoDetailViewModel = Mapper.Map<PhotoDetailViewModel>(photoDto);
            List<PhotoCommentDTO> photoCommentsDto = _commentServices.GetCommentsByPhoto(id).OrderByDescending(x => x.Created).ToList();
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
                    photoDto.ImageId = _imageServices.SaveImageToDb(file.InputStream, Enums.ImageSize.Actual);
                    photoDto.SmallImageId = _imageServices.SaveImageToDb(file.InputStream, Enums.ImageSize.Small);
                    photoDto.MiddleImageId = _imageServices.SaveImageToDb(file.InputStream, Enums.ImageSize.Middle);
                    photoDto.Created = DateTime.Now;
                }
                _photoServices.CreatePhoto(photoDto);
                return RedirectToAction("Index");
            }
            return View(photoDetailViewModel);
        }

        // GET: /Album/Edit/5
        public ActionResult Edit(Guid id)
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

        [AllowAnonymous]
        public ActionResult GetSmallPhotoImage(Guid id)
        {
            PhotoDTO photo = _photoServices.GetPhoto(id);
            return File(_imageServices.GetImageBytesFromDb(photo.SmallImageId), "image/jpg");
        }

        [AllowAnonymous]
        public ActionResult GetMiddleImage(Guid id)
        {
            PhotoDTO photo = _photoServices.GetPhoto(id);
            return File(_imageServices.GetImageBytesFromDb(photo.MiddleImageId), "image/jpg");
        }

        [AllowAnonymous]
        public ActionResult GetImage(Guid id)
        {
            PhotoDTO photo = _photoServices.GetPhoto(id);
            return File(_imageServices.GetImageBytesFromDb(photo.ImageId), "image/jpg");
        }

        [AllowAnonymous]
        public ActionResult LoadPhotosToAlbumDetailView(string albumId, string inAlbum, string pageNumber)
        {
            Guid albId = Guid.Parse(albumId);
            int pageNum = int.Parse(pageNumber);
            bool inAlb = bool.Parse(inAlbum);
            List<PhotoDTO> photoDtoList = _photoServices.GetPhotosForCurrentUserAndAlbumWithPagination(albId, inAlb, pageNum);
            return PartialView("_PhotosInAlbum", Mapper.Map<List<PhotoDTO>, List<PhotoListViewModel>>(photoDtoList));
        }

        [AllowAnonymous]
        public JsonResult GetDataForPagination(string albumId, string inAlbum, string pageNumber)
        {
            int pageSize = int.Parse(ConfigurationManager.AppSettings["AlbumDetailsPhotosPageSize"]);
            Guid albId = Guid.Parse(albumId);
            int pageNum = int.Parse(pageNumber);
            bool inAlb = bool.Parse(inAlbum);
            if (inAlb)
            {
                var result = new
                {
                    AlbumPhotosPreviousePage = pageNum - 1,
                    AlbumPhotosNextPage = decimal.Divide(_photoServices.GetPhotosNumberForAlbum(albId, true), pageSize) > pageNum ? pageNum + 1 : -1
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    AddPhotosPreviousePage = pageNum - 1,
                    AddPhotosNextPage = decimal.Divide(_photoServices.GetPhotosNumberForAlbum(albId, false), pageSize) > pageNum ? pageNum + 1 : -1
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(PhotoCommentViewModel photoCommentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("EmptyMessageError", "You can't post empty message");
                return RedirectToAction("Details", new { id = photoCommentViewModel.PhotoId });
            }
            PhotoCommentDTO photoCommentDto = Mapper.Map<PhotoCommentDTO>(photoCommentViewModel);
            _commentServices.CreateComment(photoCommentDto);
            return RedirectToAction("Details", new { id = photoCommentViewModel.PhotoId });
        }

        [HttpPost]
        public ActionResult SearchPhoto(string photoSearchText)
        {
            IEnumerable<PhotoDTO> photoDtoList = _photoServices.SearchPhotos(photoSearchText);
            if (photoDtoList.Any())
            {
                return PartialView("_PhotosList", Mapper.Map<IEnumerable<PhotoDTO>, IEnumerable<PhotoListViewModel>>(photoDtoList));
            }
            return PartialView("_NoPhotosFound");
        }

        [HttpPost]
        public ActionResult SearchPhotoExtended(
            string title,
            string takenDate,
            string place,
            string camera,
            string focalLength,
            string aperture,
            string cameraLockSpeed,
            string iso,
            string usedFlash
            )
        {
            IEnumerable<PhotoDTO> photoDtoList = _photoServices.SearchPhotosExtended(
                    title.Trim(),
                    takenDate.Trim(),
                    place.Trim(),
                    camera.Trim(),
                    focalLength.Trim(),
                    aperture.Trim(),
                    cameraLockSpeed.Trim(),
                    iso.Trim(),
                    usedFlash.Trim()
                );
            if (photoDtoList.Any())
            {
                return PartialView("_PhotosList", Mapper.Map<IEnumerable<PhotoDTO>, IEnumerable<PhotoListViewModel>>(photoDtoList));
            }
            return PartialView("_NoPhotosFound");
        }
    }
}