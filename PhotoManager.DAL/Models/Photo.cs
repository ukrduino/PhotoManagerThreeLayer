using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManager.DAL.Models.Interfaces;

namespace PhotoManager.DAL.Models
{
    public class Photo: IHasModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
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
        public bool AnyOneCanSee { get; set; }
        public int UserId { get; set; }
        public Guid SmallImageId { get; set; }
        public Guid MiddleImageId { get; set; }
        public Guid ImageId { get; set; }
        public List<Album> Albums { get; set; }
        public List<PhotoComment> Comments { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime Created { get; set; }
        public int? Views { get; set; }

        public Photo()
        {
            Albums = new List<Album>();
            Comments = new List<PhotoComment>();
        }
    }
}