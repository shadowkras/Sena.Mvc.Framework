using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Vigente.Framework.Core.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Serializa um objeto como MemoryStream (classe precisa do atributo [Serializable]).
        /// </summary>
        /// <param name="objectType">Objeto a ser serializado.</param>
        /// <returns>O objeto serializado como MemoryStream.</returns>
        public static MemoryStream SerializeToStream(this object objectType)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objectType);
            return stream;
        }

        /// <summary>
        /// Deserializa uma stream como objeto.
        /// </summary>
        /// <param name="stream">A stream a ser deserializada.</param>
        /// <returns>O objeto deserializado</returns>
        public static object DeserializeFromStream(this Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);

            object objectType = formatter.Deserialize(stream);
            return objectType;
        }

        /// <summary>
        /// Converte uma stream em uma array de bytes.
        /// </summary>
        /// <param name="stream">Stream a ser convertida.</param>
        /// <returns></returns>
        public static byte[] ReadToEnd(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
