using System.Collections.Generic;
using System.IO;
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
    public class AlbumController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BLLImageServices _imageServices = new BLLImageServices();

        public ActionResult Index()
        {
            IEnumerable<AlbumDTO> albumDtoList = _albumServices.GetAllAlbums(); //TODO server side pagination or limit number of albums or by user (NOT ALL)
            return View(Mapper.Map<IEnumerable<AlbumDTO>, IEnumerable<AlbumListViewModel>>(albumDtoList));
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
            List<PhotoDTO> photoDtoList = _photoServices.GetPhotosByAlbum(id);
            albumDetailViewModel.Photos = Mapper.Map<List<PhotoDTO>, List<PhotoListViewModel>>(photoDtoList);
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
        public ActionResult Create(AlbumDetailViewModel viewAlbum, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(viewAlbum);
                byte[] imageData = null;
                if (file != null)
                {
                    if (file.ContentLength > (500 * 1024))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File size must be less than 500 Kb");
                        return View(viewAlbum);
                    }
                    if (!file.IsJpgImage())
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File type allowed : jpeg");
                        return View(viewAlbum);
                    }
                    albumDto.ImageId = _imageServices.SaveImageToDb(file.InputStream, Enums.ImageSize.Small);
                }
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
        public ActionResult Edit(AlbumDetailViewModel viewAlbum, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(viewAlbum);
                albumDto = _albumServices.GetAlbum(viewAlbum.Id);
                AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
                if (file != null)
                {
                    if (file.ContentLength > (500 * 1024))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File size must be less than 500 Kb");
                        return View(albumDetailViewModel);
                    }
                    if (!file.IsJpgImage())
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File type allowed : jpeg");
                        return View(albumDetailViewModel);
                    }
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                    //albumDto.ImageId = _imageServices.SaveImageToDb(imageData, Enums.ImageSize.Small);
                }
                _albumServices.UpdateAlbum(albumDto);
                return RedirectToAction("Details", new { id = viewAlbum.Id });
            }
            return View(viewAlbum);
        }

        // POST: /Album/RemovePhotosFromAlbum
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemovePhotosFromAlbum(int albumId, string photosArr)
        {
            List<int> photoIds = photosArr.Split(',').ToList().Select(int.Parse).ToList();
            _albumServices.RemovePhotosFromAlbum(albumId, photoIds);
            return RedirectToAction("Details", new { id = albumId });
        }


        //TODO needs refactoring, album should contain not image but link on photo from this album
        public ActionResult GetAlbumCoverImage(int id)
        {
            return File(_imageServices.GetImageBytesFromDb(id), "image/jpg");
        }
    }
}