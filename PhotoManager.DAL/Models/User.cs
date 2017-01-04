using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoManager.DAL.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [StringLength(15)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
