using System.Collections.Generic;
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
    }
}