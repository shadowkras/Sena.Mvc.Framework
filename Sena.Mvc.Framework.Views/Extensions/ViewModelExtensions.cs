using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Extension methods for view models.
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Returns the value of the Name property of the Display attribute of an object.
        /// </summary>
        /// <param name="entity">Object with our data.</param>
        /// <param name="propertyName">Name of the property to be found.</param>
        /// <returns></returns>
        public static string DisplayName<TEntity>(this TEntity entity, string propertyName) where TEntity: class
        {
            if (string.IsNullOrWhiteSpace(propertyName.Trim()) == true)
            {
                throw new Exception($"No property name informed for ViewModelExtensions.DisplayName().");
            }

            MemberInfo property = (entity.GetType()).GetProperty(propertyName.Trim());

            if (property == null)
                throw new Exception($"No property with the Name {propertyName} found on class {entity.ToString()}.");

            if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute dd)
            {
                return dd.Name ?? property.Name;
            }
            else
            {
                return property.Name;
            }
        }
    }
}
