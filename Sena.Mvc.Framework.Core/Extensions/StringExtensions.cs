using System;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods for string objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Remove empty spaces in a string
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns></returns>
        public static string RemoveSpace(this string value)
        {
            return Regex.Replace(value, @"\s+", string.Empty);
        }

        /// <summary>
        /// Replaces all instances of a specific character in a string for empty space.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="caractere">Character to be replaced.</param>
        /// <returns></returns>
        public static string ReplaceAll(this string value, string caractere)
        {
            return ReplaceAll(value, caractere, new System.Collections.Generic.List<char>());
        }

        /// <summary>
        /// Replaces all instances of a specific character in a string for empty space.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="caractere">Character to be replaced.</param>
        /// <param name="filtro">List of characters to be ignored.</param>
        /// <returns></returns>
        public static string ReplaceAll(this string value, string caractere, System.Collections.Generic.List<char> filtro)
        {
            var builder = new System.Text.StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                if (filtro.Contains(value[i]))
                {
                    builder.Append(value[i]);
                }
                else
                {
                    builder.Append(caractere);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a string into a base 64 byte array.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// A base 64 byte array with the converted value.
        /// </returns>
        public static byte[] AsStringBase64(this string value)
        {
            byte[] result = null;

            if (value != null)
            {
                value = Regex.Replace(value, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                result = Convert.FromBase64String(value);
            }

            return result;
        }

        /// <summary>
        /// Converts a string into an UTF8 byte array.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// A byte array with the converted value.
        /// </returns>
        public static byte[] AsByteArray(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Converts a string to long.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static long AsLong(this string value)
        {
            return value.AsLong(0);
        }

        /// <summary>
        /// Converts a string to integer.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static int AsInt(this string value)
        {
            return value.AsInt(0);
        }

        /// <summary>
        /// Converts a string to an integer and specifies a default value.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or is an invalid value.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static short AsShort(this string value, short defaultValue)
        {
            if (!short.TryParse(value, out short result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.Int16" /> number.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static short AsShort(this string value)
        {
            return value.As<short>();
        }

        /// <summary>
        /// Converts a string to a a long and specifies a default value.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or is an invalid value.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static long AsLong(this string value, long defaultValue)
        {
            if (!long.TryParse(value, out long result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to an integer and specifies a default value.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or is an invalid value.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static int AsInt(this string value, int defaultValue)
        {
            if (!int.TryParse(value, out int result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.Decimal" /> number.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static decimal AsDecimal(this string value)
        {
            return value.As<decimal>();
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.Decimal" /> number and specifies a default value.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or invalid.</param>
        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            return value.As(defaultValue);
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.Single" /> number.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static float AsFloat(this string value)
        {
            return value.AsFloat(0.0f);
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.Single" /> number and specifies a default value.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null.</param>
        public static float AsFloat(this string value, float defaultValue)
        {
            if (!float.TryParse(value, out float result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.DateTime" /> value.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static DateTime AsDateTime(this string value)
        {
            return value.AsDateTime(new DateTime());
        }

        /// <summary>
        /// Converts a string to a <see cref="T:System.DateTime" /> value and specifies a default value.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">
        /// The value to return if <paramref name="value" /> is null or is an invalid value. The default
        /// is the minimum time value on the system.
        /// </param>
        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            if (!DateTime.TryParse(value, out DateTime result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to a strongly typed value of the specified data type.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="TValue">The data type to convert to.</typeparam>
        public static TValue As<TValue>(this string value)
        {
            return value.As(default(TValue));
        }

        /// <summary>
        /// Converts a string to a Boolean (true/false) value.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public static bool AsBool(this string value)
        {
            return value.AsBool(false);
        }

        /// <summary>
        /// Converts a string to a Boolean (true/false) value and specifies a default value.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or is an invalid value.</param>
        public static bool AsBool(this string value, bool defaultValue)
        {
            if (!bool.TryParse(value, out bool result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts a string to the specified data type and specifies a default value.
        /// </summary>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null.</param>
        /// <typeparam name="TValue">The data type to convert to.</typeparam>
        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                var converter1 = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter1.CanConvertFrom(typeof(string)))
                    return (TValue)converter1.ConvertFrom(value);

                var converter2 = TypeDescriptor.GetConverter(typeof(string));
                if (converter2.CanConvertTo(typeof(TValue)))
                    return (TValue)converter2.ConvertTo(value, typeof(TValue));
            }
            catch
            {
                // ignored
            }

            return defaultValue;
        }

        /// <summary>
        /// Checks whether a string can be converted to the Boolean (true/false) type.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result);
        }

        /// <summary>
        /// Checks whether a string can be converted to a long.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsLong(this string value)
        {
            long result;
            return long.TryParse(value, out result);
        }

        /// <summary>
        /// Checks whether a string can be converted to an integer.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsInt(this string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        /// <summary>
        /// Checks whether a string can be converted to the <see cref="T:System.Decimal" /> type.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsDecimal(this string value)
        {
            return value.Is<decimal>();
        }

        /// <summary>
        /// Checks whether a string can be converted to the <see cref="T:System.Single" /> type.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsFloat(this string value)
        {
            float result;
            return float.TryParse(value, out result);
        }

        /// <summary>
        /// Checks whether a string can be converted to the <see cref="T:System.DateTime" /> type.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        /// <summary>
        /// Checks whether a string can be converted to the specified data type.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The value to test.</param>
        /// <typeparam name="TValue">The data type to convert to.</typeparam>
        public static bool Is<TValue>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TValue));

            try
            {
                if (value != null)
                {
                    if (!converter.CanConvertFrom(typeof(string)))
                        return false;
                }
                converter.ConvertFrom(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks wether the object's type is a number (short, int, long, decimal, etc).
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public static bool IsNumericType(this Type propertyType)
        {
            var tipo = propertyType;

            if (propertyType.IsGenericType &&
                propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                tipo = propertyType.GetGenericArguments()[0];

            switch (Type.GetTypeCode(tipo))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks wether the object's value is a number (short, int, long, decimal, etc).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumberValue(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        /// <summary>
        /// Convert an object into another type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object value, T targetType)
        {
            // targetType above is just for compiler magic
            // to infer the type to cast value to
            return (T)value;
        }

        /// <summary>
        /// Returns the first n characters to the left side of a string.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="length">The number of characters to return.</param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            try
            {
                if (value.Length < length)
                    length = value.Length;

                return value.Substring(0, length);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns all the characters to the left side of a string, starting on a specific character.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="startingPoint">Character that is our starting point.</param>
        /// <returns></returns>
        public static string Left(this string value, char startingPoint)
        {
            return Left(value, startingPoint.ToString());
        }

        /// <summary>
        /// Returns all the characters to the left side of a string, starting on a specific string.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="startingPoint">String that is our starting point.</param>
        /// <returns></returns>
        public static string Left(this string value, string startingPoint)
        {
            try
            {
                string retorno = value;
                int idx = value.IndexOf(startingPoint);
                if (idx != -1)
                {
                    retorno = value.Substring(0, idx);
                }
                return retorno;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the first n characters to the right side of a string.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="length">The number of characters to return.</param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            try
            {
                if (value.Length < length)
                    length = value.Length;

                return value.Substring(value.Length - length, length);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns all the characters to the right side of a string, starting on a specific character.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="startingPoint">Character that is our starting point.</param>
        /// <returns></returns>
        public static string Right(this string value, char startingPoint)
        {
            return Right(value, startingPoint.ToString());
        }

        /// <summary>
        /// Returns all the characters to the right side of a string, starting on a specific string.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="startingPoint">String that is our starting point.</param>
        /// <returns></returns>
        public static string Right(this string value, string startingPoint)
        {
            try
            {
                string retorno = value;
                int idx = value.IndexOf(startingPoint);
                if (idx != -1)
                {
                    retorno = value.Substring(idx + startingPoint.Length);
                }
                return retorno;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the first n characters to the right side of a string, starting on a specific character.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <param name="startingPoint">Char that is our starting point.</param>
        /// <param name="length">The number of characters to return.</param>
        /// <returns></returns>
        public static string Right(this string value, char startingPoint, int length)
        {
            try
            {
                string retorno = value;
                int idx = -1;
                while (length > 0)
                {
                    idx = value.IndexOf(startingPoint, idx + 1);
                    if (idx == -1)
                    {
                        break;
                    }
                    --length;
                }

                if (idx != -1)
                {
                    retorno = value.Substring(idx + 1);
                }

                return retorno;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns if our string is null or empty.
        /// </summary>
        /// <param name="value">String of characters.</param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
