using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManager.DAL.Models.Interfaces;

namespace PhotoManager.DAL.Models
{
    public class PhotoComment : IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int PhotoID { get; set; }
        public Photo Photo { get; set; }
    }
}
