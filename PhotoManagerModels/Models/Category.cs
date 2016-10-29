using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoManagerModels.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Photo> Photoes { get; set; }
        public List<Album> Albums { get; set; }


        public Category(string title)
        {
            Title = title;
        }
        public Category()
        {
        }
    }
}