using System;
using System.Collections.Generic;
using PhotoManager.DAL.Models;

namespace PhotoManager.BLL.DTOModels
{
    public class PhotoDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TakenDate { get; set; }
        public string Place { get; set; }
        public string FocalLength { get; set; }
        public string Camera { get; set; }
        public string Aperture { get; set; }
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        public bool UsedFlash { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public List<Album> AlbumsIds { get; set; }
        public List<PhotoComment> CommentsIds { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime Created { get; set; }
        public bool AnyOneCanSee { get; set; }
        public int? Views { get; set; }
        public PhotoDTO()
        {
            AlbumsIds = new List<Album>();
            CommentsIds = new List<PhotoComment>();
        }
    }
}