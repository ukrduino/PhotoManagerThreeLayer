using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoManager.BLL.Utils
{
    public static class ImageUtils
    {
        //TODO define sizes
        private static int smallImageWidth = 260;
        private static int smallImageHights = 195;
        private static int middleImageWidth = 800;
        private static int middleImageHights = 600;

        private static Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage((Image)newImage);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static byte[] ResizeImage(Stream imageDataStream, Enums.ImageSize size)
        {
            using (Image img = Image.FromStream(imageDataStream))
            {
                Image newImage;
                switch (size)
                {
                    case Enums.ImageSize.Middle:
                        newImage = ResizeImage(img, middleImageWidth, middleImageHights);
                        using (var output = new MemoryStream())
                        {
                            newImage.Save(output, ImageFormat.Jpeg);
                            return output.ToArray();
                        }

                    case Enums.ImageSize.Small:
                        newImage = ResizeImage(img, smallImageWidth, smallImageHights);
                        using (var output = new MemoryStream())
                        {
                            newImage.Save(output, ImageFormat.Jpeg);
                            return output.ToArray();
                        }
                    default:
                        return new byte[0];
                }
            }
        }
    }
}
