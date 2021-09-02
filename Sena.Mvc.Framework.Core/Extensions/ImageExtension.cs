using System.Drawing;
using System.IO;

namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods for images.
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// Resizes an image to a lower size in kylobytes.
        /// </summary>
        /// <param name="byteImageIn">Byte array with our image.</param>
        /// <param name="byteSize">New size in bytes.</param>
        /// <returns>Returns the resized byte array.</returns>
        private static byte[] ResizeToXKbytes(this byte[] byteImageIn, int byteSize)
        {
            if (byteImageIn == null || byteImageIn?.Length == 0)
            {
                return byteImageIn;
            }

            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;

            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > byteSize)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }

        /// <summary>
        /// Resizes an image to about 50 kylobytes.
        /// </summary>
        /// <param name="byteImageIn">Byte array with our image.</param>
        /// <returns>Returns the resized byte array.</returns>
        public static byte[] ResizeTo50Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 50000);
        }

        /// <summary>
        /// Resizes an image to about 10 kylobytes.
        /// </summary>
        /// <param name="byteImageIn">Byte array with our image.</param>
        /// <returns>Returns the resized byte array.</returns>
        public static byte[] ResizeTo10Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 10000);
        }

        /// <summary>
        /// Resizes an image to about 5 kylobytes.
        /// </summary>
        /// <param name="byteImageIn">Byte array with our image.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeTo5Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 5000);
        }

        /// <summary>
        /// Resizes an image to about 1 kylobytes.
        /// </summary>
        /// <param name="byteImageIn">Byte array with our image.</param>
        /// <returns>Returns the resized byte array.</returns>
        public static byte[] ResizeTo1Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 1000);
        }

        /// <summary>
        /// Redimensiona uma imagem para até N kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <param name="n">New size in Kbytes.</param>
        /// <returns>Returns the resized byte array.</returns>
        public static byte[] ResizeToNKbytes(this byte[] byteImageIn, int n)
        {
            return ResizeToXKbytes(byteImageIn, n * 1000);
        }
    }
}
