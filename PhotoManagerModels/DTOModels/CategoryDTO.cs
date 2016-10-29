using System.Collections.Generic;

namespace PhotoManagerModels.DTOModels
{
    public class CategoryDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public List<PhotoDTO> Photoes { get; set; }
        public List<AlbumDTO> Albums { get; set; }


        public CategoryDTO(string title)
        {
            Title = title;
        }
        public CategoryDTO()
        {
        }
    }
}