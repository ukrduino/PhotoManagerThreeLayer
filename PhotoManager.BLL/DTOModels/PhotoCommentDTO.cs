using System;


namespace PhotoManager.BLL.DTOModels
{
    public class PhotoCommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
    }
}