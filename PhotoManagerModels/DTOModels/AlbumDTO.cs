using System;
using System.Collections.Generic;
using PhotoManagerModels.DTOModels.Interfaces;

namespace PhotoManagerModels.DTOModels
{
    public class AlbumDTO: ICommentableDTO, ICategorizedDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<PhotoDTO> Photos { get; set; }
        public byte[] CoverImageData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

        public AlbumDTO()
        {
            Photos = new List<PhotoDTO>();
            Categories = new List<CategoryDTO>();
            Comments = new List<CommentDTO>();
        }
    }
}