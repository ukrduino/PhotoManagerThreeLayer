using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManager.DAL.Models.Interfaces;

namespace PhotoManager.DAL.Models
{
    public class Album: IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public byte[] CoverImageData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

        public Album()
        {
            CreatedDate = DateTime.Now;
        }
    }
}