using System;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class AlbumListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? ImageId { get; set; }
    }
}