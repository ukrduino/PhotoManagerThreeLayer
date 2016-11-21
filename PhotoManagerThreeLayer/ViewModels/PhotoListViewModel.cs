using System;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class PhotoListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] SmallSizeImageData { get; set; }
        public bool anyOneCanSee { get; set; }
        public DateTime Uploaded { get; set; }
        public int Views { get; set; }
        public int Comments { get; set; }
    }
}
