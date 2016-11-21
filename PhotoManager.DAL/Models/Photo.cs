using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManager.DAL.Models.Interfaces;

namespace PhotoManager.DAL.Models
{
    public class Photo: IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public DateTime? TakenDate { get; set; }
        public List<PhotoComment> Comments { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Camera { get; set; }
        public string FocalLength { get; set; }
        public string Aperture { get; set; }
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        public bool UsedFlash { get; set; }
        public byte[] OriginalSizeImageData { get; set; }
        public byte[] MiddleSizeImageData { get; set; }
        public byte[] SmallSizeImageData { get; set; }
        public List<Album> Albums { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime Uploaded { get; set; }
        public bool AnyOneCanSee { get; set; }
        public int Views { get; set; }

        public Photo()
        {
            Albums = new List<Album>();
            Comments = new List<PhotoComment>();
            Uploaded = DateTime.Now;
            AnyOneCanSee = true;
        }
    }
}