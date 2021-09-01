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
        /// Redimensiona uma imagem para uma quantidade menor em kilobytes.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
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
        /// Redimensiona uma imagem para até 50kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeTo50Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 50000);
        }

        /// <summary>
        /// Redimensiona uma imagem para até 10kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeTo10Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 10000);
        }

        /// <summary>
        /// Redimensiona uma imagem para até 5kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeTo5Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 5000);
        }

        /// <summary>
        /// Redimensiona uma imagem para até 1kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeTo1Kbytes(this byte[] byteImageIn)
        {
            return ResizeToXKbytes(byteImageIn, 1000);
        }

        /// <summary>
        /// Redimensiona uma imagem para até N kb.
        /// </summary>
        /// <param name="byteImageIn">Array de bytes da imagem.</param>
        /// <param name="n">Tamanho em Kbytes.</param>
        /// <returns>Retorna uma nova array redimensionada.</returns>
        public static byte[] ResizeToNKbytes(this byte[] byteImageIn, int n)
        {
            return ResizeToXKbytes(byteImageIn, n * 1000);
        }
    }
}
