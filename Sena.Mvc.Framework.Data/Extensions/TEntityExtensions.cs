using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Sena.Mvc.Framework.Data.Interfaces;

namespace Sena.Mvc.Framework.Data.Extensions
{
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
        /// Realiza o mapeamento as propriedades da classe de origem utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de Origem com as informações das propriedades.</param>
        /// <param name="predicado">Predicado com o construtor do objeto de destino.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
        public static TDestination AutoMapear<TDestination, TSource>(this TSource source, Expression<Func<TSource, TDestination>> predicado) where TSource : class, IAutoMappleable
                                                                                                                                             where TDestination : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ConstructUsing(predicado);
            });

            return configuracao.CreateMapper().Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Realiza o mapeamento as propriedades da classe de origem utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de origem com as informações das propriedades.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
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
        /// Realiza o mapeamento de uma lista de objetos utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de origem com as informações das propriedades.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
        public static IEnumerable<TDestination> AutoMapearLista<TSource, TDestination>(this IEnumerable<TSource> source) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        /// <summary>
        /// Realiza o mapeamento de uma lista de objetos utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de origem com as informações das propriedades.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
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
        /// Realiza o mapeamento de um predicado da classe de origem para a classe de destino utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Objeto de destino.</typeparam>
        /// <typeparam name="TSource">Objeto de origem.</typeparam>
        /// <param name="predicado">Predicado do objeto de origem.</param>
        /// <returns>Retorna um predicado com o objeto de destino.</returns>
        public static Expression<Func<TDestination, bool>> MapearPredicado<TSource, TDestination>(this Expression<Func<TSource, bool>> predicado) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TDestination, TSource>();
            });

            return configuracao.CreateMapper().Map<Expression<Func<TSource, bool>>, Expression<Func<TDestination, bool>>>(predicado);
        }

        #endregion

        #region Verificar implementação de interfaces

        /// <summary>
        /// Verifica se a entidade implementa uma interface.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade de origem.</typeparam>
        /// <typeparam name="TInterface">Tipo da interface.</typeparam>
        /// <param name="entity">Entidade de origem.</param>
        /// <returns></returns>
        public static bool Implements<TEntity, TInterface>(this TEntity entity)
        {
            return typeof(TInterface).IsAssignableFrom(entity.GetType());
        }

        #endregion        
    }
}
