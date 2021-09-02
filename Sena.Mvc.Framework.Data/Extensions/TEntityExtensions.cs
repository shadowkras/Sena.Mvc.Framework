using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Sena.Mvc.Framework.Data.Interfaces;

namespace Sena.Mvc.Framework.Data.Extensions
{
    /// <summary>
    /// Extension methods for TEntity objects, which must inherit the interface IAutoMappleable.
    /// </summary>
    public static class TEntityExtensions
    {
        #region Dettach do entity framework

        /// <summary>
        /// Detaches an entity from a DbContext, disabling lazy loading.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        /// <param name="context">DbContext that controls the entity.</param>
        /// <returns></returns>
        internal static TEntity Detach<TEntity>(this TEntity entity, DbContext context) where TEntity : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        #endregion

        #region Auto-mapear

        /// <summary>
        /// Maps the properties of a class using AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination class.</typeparam>
        /// <typeparam name="TSource">Type of the source class.</typeparam>
        /// <param name="source">Source object with information..</param>
        /// <param name="predicate">Predicate with the construction of the destination class.</param>
        /// <returns>Object of the destination type.</returns>
        public static TDestination AutoMapear<TDestination, TSource>(this TSource source, Expression<Func<TSource, TDestination>> predicate) where TSource : class, IAutoMappleable
                                                                                                                                             where TDestination : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ConstructUsing(predicate);
            });

            return configuracao.CreateMapper().Map<TSource, TDestination>(source);
        }

        /// <summary>
        ///  Maps the properties of a class using AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination class.</typeparam>
        /// <typeparam name="TSource">Type of the source class.</typeparam>
        /// <param name="source">Source object with information.</param>
        /// <returns>Object of the destination type.</returns>
        public static TDestination AutoMapear<TSource, TDestination>(this TSource source) where TSource : class, IAutoMappleable
                                                                                          where TDestination: class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Maps an ienumerable object using AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination class.</typeparam>
        /// <typeparam name="TSource">Type of the source class.</typeparam>
        /// <param name="source">Source object with information..</param>
        /// <returns>IEnumerable list of objects of the destination type.</returns>
        public static IEnumerable<TDestination> AutoMapearLista<TSource, TDestination>(this IEnumerable<TSource> source) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        /// <summary>
        /// Maps a list of objects using AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination class.</typeparam>
        /// <typeparam name="TSource">Type of the source class.</typeparam>
        /// <param name="source">Source object with information..</param>
        /// <returns>IList of objects of the destination type.</returns>
        public static IList<TDestination> AutoMapearLista<TSource, TDestination>(this IList<TSource> source) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<IList<TSource>, IList<TDestination>>(source);
        }

        #endregion

        #region Mapear Predicado

        /// <summary>
        /// Maps an expression predicate from the source class to the destination class.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination class.</typeparam>
        /// <typeparam name="TSource">Type of the source class.</typeparam>
        /// <param name="predicate">Expression Predicate with the source object.</param>
        /// <returns>Returns an expression predicate with the destination object.</returns>
        public static Expression<Func<TDestination, bool>> MapearPredicado<TSource, TDestination>(this Expression<Func<TSource, bool>> predicate) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TDestination, TSource>();
            });

            return configuracao.CreateMapper().Map<Expression<Func<TSource, bool>>, Expression<Func<TDestination, bool>>>(predicate);
        }

        #endregion

        #region Verificar implementação de interfaces

        /// <summary>
        /// Checks if an entity implements a specific interface.
        /// </summary>
        /// <typeparam name="TEntity">Type of the source entity.</typeparam>
        /// <typeparam name="TInterface">Type of the interface to be implemented.</typeparam>
        /// <param name="entity">Source entity.</param>
        /// <returns></returns>
        public static bool Implements<TEntity, TInterface>(this TEntity entity)
        {
            return typeof(TInterface).IsAssignableFrom(entity.GetType());
        }

        #endregion        
    }
}
