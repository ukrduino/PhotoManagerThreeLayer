using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PhotoManager.BLL.Services;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.ViewModels;

namespace PhotoManagerThreeLayer.Controllers
{
    public class AlbumController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BllCategoryServices _categoryServices = new BllCategoryServices();
        //private BllCommentServices _commentServices = new BllCommentServices();

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
            List<CategoryDTO> categoriesDtoList = _categoryServices.GetCategoriesByAlbum(id);
            //List<CommentDTO> commentsDtoList = _commentServices.GetCommentsByAlbum(id);
            albumDetailViewModel.Photos = Mapper.Map<List<PhotoDTO>, List<PhotoListViewModel>>(photoDtoList);
            albumDetailViewModel.Categories = Mapper.Map<List<CategoryDTO>, List<CategoryViewModel>>(categoriesDtoList);
            //albumDetailViewModel.Comments = Mapper.Map<List<CommentDTO>, List<CommentViewModel>(commentsDtoList);
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
        public ActionResult Create(AlbumDetailViewModel album, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (file != null)
                {
                    if (file.ContentLength > (500 * 1024))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File size must be less than 500 Kb");
                        return View(album);
                    }
                    if (!file.ContentType.Equals("image/jpeg"))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File type allowed : jpeg");
                        return View(album);
                    }
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(album);
                albumDto.CoverImageData = imageData;
                _albumServices.CreateAlbum(albumDto);
                return RedirectToAction("Index");
            }
            return View(album);
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
        public ActionResult Edit(AlbumDetailViewModel album, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                AlbumDTO albumDto = Mapper.Map<AlbumDTO>(album);
                if (file != null)
                {
                    if (file.ContentLength > (500 * 1024))
                    {
                        albumDto = _albumServices.GetAlbum(album.Id);
                        AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
                        ModelState.AddModelError("ImageUploadValidationError", "File size must be less than 500 Kb");
                        return View(albumDetailViewModel);
                    }
                    if (!file.ContentType.Equals("image/jpeg"))
                    {
                        ModelState.AddModelError("ImageUploadValidationError", "File type allowed : jpeg");
                        return View(album);
                    }
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                    albumDto.CoverImageData = imageData;
                }
                _albumServices.UpdateAlbum(albumDto);
                return RedirectToAction("Details", new { id = album.Id });
            }
            return View(album);
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
    }
}