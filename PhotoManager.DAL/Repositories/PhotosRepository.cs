using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LinqKit;
using PhotoManager.DAL.Models;


namespace PhotoManager.DAL.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private PhotoManagerDbContext _context;

        public PhotoRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Photo> GetPhotosByAlbum(Guid id)
        {
            var query = from photo in _context.Photos
                        where photo.Albums.Any(album => album.Id == id)
                        select photo;
            return query.ToList();
        }

        public List<Photo> GetPhotosByUserId(int userId)
        {
            var query = from photos in _context.Photos
                        where photos.UserId == userId
                        select photos;
            return query.ToList();
        }
        public Photo GetPhotoById(Guid id)
        {
            return _context.Photos.FirstOrDefault(photo => photo.Id == id);
        }

        public void UpdatePhoto(Photo photo)
        {
            _context.Photos.Attach(photo);
            var photoFromDb = _context.Entry(photo);
            photoFromDb.State = EntityState.Modified;
            photoFromDb.Property(phot => phot.Created).IsModified = false;
            photoFromDb.Property(phot => phot.UserId).IsModified = false;
            photoFromDb.Property(phot => phot.ImageId).IsModified = false;
        }

        public List<Photo> GetPhotosByUserAndAlbum(int userId, Guid albumId, bool inAlbum, bool excludePrivate)
        {
            List<Photo> photos;
            photos = _context.Photos.Where(photo => photo.UserId.Equals(userId)).Include("Albums").ToList();
            if (excludePrivate)
            {
                photos.RemoveAll(photo => photo.AnyOneCanSee == false);
            }
            if (inAlbum)
            {
                return photos.Where(phot => phot.Albums.Any(alb => alb.Id.Equals(albumId))).ToList();
            }
            return photos.Where(phot => !phot.Albums.Any(alb => alb.Id.Equals(albumId))).ToList();
        }

        public List<Photo> GetPhotosByUserAndAlbumWithPagination(int userId, Guid albumId, bool inAlbum, int skip = 0, int pageSize = 0)
        {
            List<Photo> photos = _context.Photos.Where(photo => photo.UserId.Equals(userId)).Include("Albums").ToList();
            if (inAlbum)
            {
                return photos.Where(phot => phot.Albums.Any(alb => alb.Id.Equals(albumId))).Skip(skip).Take(pageSize).ToList();
            }
            return photos.Where(phot => !phot.Albums.Any(alb => alb.Id.Equals(albumId))).Skip(skip).Take(pageSize).ToList();
        }

        public List<Photo> SearchPhotos(string photoSearchText, int userId)
        {
            List<Photo> res1 = _context.SearchPhotos(photoSearchText, userId);
            return res1;
        }

        public List<Photo> SearchPhotosExtended(
            string title,
            DateTime takenDate,
            string place,
            string camera,
            string focalLength,
            string aperture,
            string cameraLockSpeed,
            string iso,
            string usedFlash,
            int userId
            )
        {
            var predicate = PredicateBuilder.True<Photo>();
            predicate = predicate.And(photo => photo.UserId == userId);
            if (!string.IsNullOrEmpty(title)) predicate = predicate.And(photo => photo.Title == title);
            if (takenDate != DateTime.MinValue) predicate = predicate.And(photo => photo.TakenDate == takenDate);
            if (!string.IsNullOrEmpty(place)) predicate = predicate.And(photo => photo.Place == place);
            if (!string.IsNullOrEmpty(camera)) predicate = predicate.And(photo => photo.Camera == camera);
            if (!string.IsNullOrEmpty(focalLength)) predicate = predicate.And(photo => photo.FocalLength == focalLength);
            if (!string.IsNullOrEmpty(aperture)) predicate = predicate.And(photo => photo.Aperture == aperture);
            if (!string.IsNullOrEmpty(cameraLockSpeed)) predicate = predicate.And(photo => photo.CameraLockSpeed == cameraLockSpeed);
            if (!string.IsNullOrEmpty(iso)) predicate = predicate.And(photo => photo.ISO == iso);
            if (!usedFlash.Equals("notSearch")) predicate = predicate.And(photo => photo.UsedFlash == usedFlash.Equals("used"));

            return _context.Photos.AsExpandable().Where(predicate).ToList();
        }
    }
}