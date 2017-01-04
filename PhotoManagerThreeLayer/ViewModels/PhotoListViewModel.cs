using System;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class PhotoListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AnyOneCanSee { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }
        public int CommentsNumber { get; set; }
    }
}
