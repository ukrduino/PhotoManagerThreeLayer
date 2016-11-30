using System;


namespace PhotoManager.BLL.DTOModels
{
    public class PhotoCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PhotoId { get; set; }
    }
}