using System;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class PhotoListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SmallImageId { get; set; }
        public bool AnyOneCanSee { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }
        public int Comments { get; set; }
    }
}
