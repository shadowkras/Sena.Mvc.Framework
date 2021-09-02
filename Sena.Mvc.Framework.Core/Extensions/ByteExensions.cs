namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods for byte[] objects.
    /// </summary>
    public static class ByteExensions
    {
        /// <summary>
        /// Converts a byte array into an UTF8 string.
        /// </summary>
        /// <param name="value">Byte array with data.</param>
        /// <returns>
        /// UTF8 string with converted data.
        /// </returns>
        public static string AsString(this byte[] value)
        {
            var result = string.Empty;

            if (value != null)
                result = System.Text.Encoding.UTF8.GetString(value);

            return result;
        }

        /// <summary>
        /// Converts a byte array into a base 64 string.
        /// </summary>
        /// <param name="value">Byte array with data.</param>
        /// <returns>
        /// Base 64 string with converted data.
        /// </returns>
        public static string AsStringBase64(this byte[] value)
        {
            var result = string.Empty;

            if (value != null && value.Length > 0)
                result = string.Format("data:image/png;base64,{0}", System.Convert.ToBase64String(value));

            return result;
        }
    }
}
