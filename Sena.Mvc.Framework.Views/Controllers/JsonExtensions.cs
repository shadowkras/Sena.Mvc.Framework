using System;
using Newtonsoft.Json;
using Sena.Mvc.Framework.Core.Extensions;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Extension methods to work with Json strings.
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Convert a TEntity object into a JSON string.
        /// </summary>
        /// <param name="source">Object of TEntity type.</param>
        /// <param name="loadNullProperties">Defines if properties with a null value should be serialize.</param>
        /// <returns></returns>
        public static string SerializeJSON<TEntity>(this TEntity source, bool loadNullProperties = false)
        {
            string json = string.Empty;

            try
            {
                if (loadNullProperties == true)
                {
                    json = JsonConvert.SerializeObject(source, Helpers.JsonConverters.DefaultSettingsWithNulls);
                }
                else
                {
                    json = JsonConvert.SerializeObject(source, Helpers.JsonConverters.DefaultSettings);
                }
            }
            catch (JsonSerializationException Ex)
            {
                throw new Exception("Erro ao serializar o objeto em JSON: " + Ex.GetMessages());
            }
            catch (Exception Ex)
            {
                throw new Exception("Erro durante a serialização do objeto " + source.GetType() + ": " + Ex.GetMessages());
            }

            return json;
        }

        /// <summary>
        /// Converts a JSON string into an object of the TEntity type.
        /// </summary>
        /// <param name="source">JSON string.</param>
        /// <param name="loadNullProperties">Defines if properties with a null value should be serialize.</param>
        /// <returns></returns>
        public static TEntity DeserializeJSON<TEntity>(this string source, bool loadNullProperties = false)
        {
            if (source == null)
            {
                throw new Exception("Uma string JSON nula não pode ser deserializada.");
            }

            try
            {
                if (loadNullProperties == true)
                {
                    var json = JsonConvert.DeserializeObject(source, typeof(TEntity), Helpers.JsonConverters.DefaultSettingsWithNulls);
                    return (TEntity)json;
                }
                else
                {
                    var json = JsonConvert.DeserializeObject(source, typeof(TEntity), Helpers.JsonConverters.DefaultSettings);
                    return (TEntity)json;
                }
            }
            catch (JsonSerializationException Ex)
            {
                throw new Exception("Erro ao deserializar o arquivo JSON: " + Ex.GetMessages());
            }
            catch (Exception Ex)
            {
                throw new Exception("Erro ao inicializar o arquivo JSON: " + Ex.GetMessages());
            }
        }
    }
}
