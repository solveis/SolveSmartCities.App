using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Common
{
    public class Helpers
    {
        public static byte[] ConvertImageToPng(byte[] image, int maxWidth, int maxHeight)
        {
            using (var originalFormatStream = new System.IO.MemoryStream(image))
            {
                using (var strongImage = Image.FromStream(originalFormatStream))
                {
                    using (var resizedImageStream = ScaleImage(strongImage, maxWidth, maxHeight))
                    {
                        return resizedImageStream.ToArray();
                    }
                }
            }
        }

        private static System.IO.MemoryStream ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            using (var bitmap = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                }

                var stream = new System.IO.MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                return stream;
            }
        }

        public static string RemoveSchemeFromUri(Uri uri)
        {
            return uri.ToString().Remove(0, uri.Scheme.Length + 1);
        }
    }
}
