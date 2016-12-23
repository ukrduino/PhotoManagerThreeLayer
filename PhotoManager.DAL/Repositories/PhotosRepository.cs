﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public List<Photo> GetPhotosByAlbum(int id)
        {
            var query = from photo in _context.Photos
                        where photo.Albums.Any(album => album.Id == id)
                        select photo;
            return query.ToList();
        }

        public List<Photo> GetPhotosByUser(User user)
        {
            var query = from photos in _context.Photos
                        where photos.UserId == user.UserId
                        select photos;
            return query.ToList();
        }
        public Photo GetPhotoById(int id)
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

        public List<Photo> GetPhotosByUserAndAlbum(User user, Album album, bool inAlbum)
        {
            List<Photo> photos = _context.Photos.Where(photo => photo.UserId.Equals(user.UserId)).Include("Albums").ToList();
            if (inAlbum)
            {
                return photos.Where(phot => phot.Albums.Any(alb => alb.Id.Equals(album.Id))).ToList();
            }
            return photos.Where(phot => !phot.Albums.Contains(album)).ToList();
        }

        public List<Photo> GetPhotosByUserAndAlbumWithPagination(User user, Album album, bool inAlbum, int skip = 0, int pageSize = 0)
        {
            List<Photo> photos = _context.Photos.Where(photo => photo.UserId.Equals(user.UserId)).Include("Albums").ToList();
            if (inAlbum)
            {
                return photos.Where(phot => phot.Albums.Any(alb=>alb.Id.Equals(album.Id))).Skip(skip).Take(pageSize).ToList();
            }
            return photos.Where(phot => !phot.Albums.Contains(album)).Skip(skip).Take(pageSize).ToList();
        }

        public List<Photo> SearchPhotos(string photoSearchText, int userId)
        {
            List<Photo> res1 = _context.SearchPhotos(photoSearchText, userId);
            return res1;
        }
    }
}