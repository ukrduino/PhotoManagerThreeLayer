using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManager.DAL.Models.Interfaces;

namespace PhotoManager.DAL.Models
{
    public class Album: IHasModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Photo> Photos { get; set; }
        public int? ImageId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public Album()
        {
            Photos = new List<Photo>();
            Created = DateTime.Now;
        }
    }
}