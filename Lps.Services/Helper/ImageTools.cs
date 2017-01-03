namespace Lps.Services.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Linq;    

    public class ImageTools
    {
        public static void ResizeWithCut(string original, string filePath, int maxWidth, int maxHeight)
        {
            using (var image = Image.FromFile(original))
            {
                ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/jpeg").First();
                Image finalImage = image;
                System.Drawing.Bitmap bitmap = null;
               
                int left = 0;
                int top = 0;
                int srcWidth = maxWidth;
                int srcHeight = maxHeight;
                bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
                double croppedHeightToWidth = (double)maxHeight / maxWidth;
                double croppedWidthToHeight = (double)maxWidth / maxHeight;

                if (image.Width > image.Height)
                {
                    ImageTools.ResizeProportionalScale(original, filePath, maxWidth, maxHeight);
                    return;
                    //srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                    //if (srcWidth < image.Width)
                    //{
                    //    srcHeight = image.Height;
                    //    left = (image.Width - srcWidth) / 2;
                    //}
                    //else
                    //{
                    //    srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                    //    srcWidth = image.Width;
                    //    top = (image.Height - srcHeight) / 2;
                    //}
                }
                else
                {
                    srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                    if (srcHeight < image.Height)
                    {
                        srcWidth = image.Width;
                        top = (image.Height - srcHeight) / 2;
                    }
                    else
                    {
                        srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                        srcHeight = image.Height;
                        left = (image.Width - srcWidth) / 2;
                    }
                }
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
                }
                finalImage = bitmap;
              

                try
                {
                    using (EncoderParameters encParams = new EncoderParameters(1))
                    {
                        encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
                        //quality should be in the range 
                        //[0..100] .. 100 for max, 0 for min (0 best compression)
                        finalImage.Save(filePath, jpgInfo, encParams);
                    }
                }
                finally
                {
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                    }
                }
            }
        }

        public static void ResizeProportionalScale(string original, string output, int width, int height)
        {
            using (var image = Image.FromFile(original))
            {
                int newWidth;
                int newHeight;

                if (image.Width > width || image.Height > height)
                {
                    int originalWidth = image.Width;
                    int originalHeight = image.Height;
                    float percentWidth = (float)width / (float)originalWidth;
                    float percentHeight = (float)height / (float)originalHeight;
                    float percent = percentHeight < percentWidth ? percentHeight : percentWidth;

                    newWidth = (int)(originalWidth * percent);
                    newHeight = (int)(originalHeight * percent);
                }
                else
                {
                    newWidth = image.Width;
                    newHeight = image.Height;
                }

                using (var thumbnail = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                    ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                    EncoderParameters encoderParameters;
                    encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    thumbnail.Save(output, info[1], encoderParameters);
                }
            }
        }
    }
}
