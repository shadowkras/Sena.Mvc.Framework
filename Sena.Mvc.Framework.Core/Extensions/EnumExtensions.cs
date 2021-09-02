using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods for Enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Display (Name) for an enumerator item.
        /// </summary>
        /// <param name="enumerador">Enumerator item.</param>
        /// <returns></returns>
        public static string EnumDescription(this Enum enumerador)
        {
            return enumerador.GetType()
                             .GetMember(enumerador.ToString())
                             .FirstOrDefault()
                             ?.GetCustomAttribute<DisplayAttribute>(false)
                             ?.Name
                             ?? enumerador.ToString();
        }

        /// <summary>
        /// Returns the values of an enum as a list.
        /// </summary>
        /// <typeparam name="T">Type of the Enum.</typeparam>
        public static IList<T> GetValues<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Values;
        }

        /// <summary>
        /// Returns a string array with the names of the items of an enum.
        /// </summary>
        /// <typeparam name="T">Type of the Enum.</typeparam>
        /// <returns></returns>
        public static string[] GetNamesArray<T>() where T : struct, IEnumConstraint
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
