using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManagerModels.Models.Interfaces;

namespace PhotoManagerModels.Models
{
    public class Photo: IHasLastModifiedField, ICommentable, ICategorized
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
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
        public DateTime CreatedDate { get; set; }

        public Photo()
        {
            Albums = new List<Album>();
            Categories = new List<Category>();
            Comments = new List<Comment>();
            CreatedDate = DateTime.Now;
        }
    }
}