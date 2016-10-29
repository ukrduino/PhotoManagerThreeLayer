
namespace PhotoManagerModels.ViewModels
{
    public class AlbumListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] CoverImageData { get; set; }

        public AlbumListViewModel()
        {
        }
    }
}