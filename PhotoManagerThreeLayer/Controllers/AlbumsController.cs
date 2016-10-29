using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoManager.BLL.Services;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.ViewModels;


namespace PhotoManager.Controllers
{
    public class AlbumsController : Controller
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        public ActionResult Index()
        {
            IEnumerable<AlbumDTO> albumDtoList = _albumServices.GetAllAlbums();
            AutoMapper.Mapper.Map<AlbumListViewModel>(albumDtoList);
            return View(AutoMapper.Mapper.Map<IEnumerable<AlbumListViewModel>>(albumDtoList));
        }

        //// GET: /Albums/Details/5
        //public ActionResult Details(int id = 0)
        //{
        //    AlbumModel album = db.Albums.Include(p=>p.Photos).FirstOrDefault(x=>x.Id==id);
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }

        //   return View(album);
        //}

        //// GET: /Albums/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Products/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(AlbumModel album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Albums.Add(album);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(album);
        //}

        //// GET: /Products/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    AlbumModel album = db.Albums.Find(id);
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(album);
        //}

        ////
        //// POST: /Products/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(AlbumModel album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(album).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(album);
        //}


        //// GET: /Products/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    AlbumModel album = db.Albums.Find(id);
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(album);
        //}

        //// POST: /Products/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    AlbumModel album = db.Albums.Find(id);
        //    db.Albums.Remove(album);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Photoes()
        //{
        //    return RedirectToAction("Index", "Albums", null);
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}