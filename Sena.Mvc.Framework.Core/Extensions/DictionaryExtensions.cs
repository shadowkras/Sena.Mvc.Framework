using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods for ICollection, IDictionary and IList objects.
    /// </summary>
    public static class DictionaryExtensions
    { 
        /// <summary>
        /// Returns if our dictionary is empty.
        /// </summary>
        /// <param name="dictionary">Object of IDictionary type.</param>
        /// <returns></returns>
        public static bool IsEmpty(this IDictionary dictionary)
        {
            return dictionary.Count == 0;
        }

        /// <summary>
        /// Returns if our list is empty.
        /// </summary>
        /// <param name="list">Object of IList type.</param>
        /// <returns></returns>
        public static bool IsEmpty(this IList list)
        {
            return list.Count == 0;
        }

        /// <summary>
        /// Returns if our collection is empty.
        /// </summary>
        /// <param name="collection">Object of ICollection type.</param>
        /// <returns></returns>
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Returns if our collection is empty.
        /// <para>Example: if(MyCollection.IsEmpty() == true)</para>
        /// </summary>
        /// <param name="collection">Object of ICollection type.</param>
        /// <returns></returns>        
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null)
                return true;
            else
                return (collection.Count == 0);
        }

        /// <summary>
        /// Returns if our IEnumerable is empty.
        /// <para>Example: if(MyEnumerable.IsEmpty() == true)</para>
        /// </summary>
        /// <param name="ienumerable">Object of IEnumerable type.</param>
        /// <returns></returns>        
        public static bool IsEmpty<T>(this IEnumerable<T> ienumerable)
        {
            if (ienumerable == null)
                return true;
            else
                return (ienumerable.Any() == false);
        }
    }
}
