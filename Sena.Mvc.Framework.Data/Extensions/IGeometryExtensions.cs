using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using Sena.Mvc.Framework.Core.Extensions;

namespace Sena.Mvc.Framework.Data.Extensions
{
    /// <summary>
    /// Classe de extenção para trabalhar com geometrias e geojson.
    /// </summary>
    public static class IGeometryExtensions
    {
        /// <summary>
        /// Método para converter uma geometria em GeoJson.
        /// </summary>
        /// <param name="geometry">Objeto do tipo IGeometry.</param>
        /// <returns></returns>
        public static  string WriteGeoJson(this GeoAPI.Geometries.IGeometry geometry)
        {
            if(geometry == null)
            {
                throw new System.Exception("Geometria sem valores para converter em GeoJson.");
            }

            try
            {
                var geometryFactory = (NetTopologySuite.Geometries.GeometryFactory)geometry.Factory;
                JsonSerializer serializer = GeoJsonSerializer.Create(geometryFactory);
                StringBuilder builder = new StringBuilder();
                using (StringWriter writter = new StringWriter(builder))
                {
                    serializer.Serialize(writter, geometry);
                }

                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar converter Geometria em GeoJson." + Environment.NewLine + ex.GetMessages());
            }
        }

        /// <summary>
        /// Método para converter um GeoJson em um objeto do tipo IGeometry.
        /// </summary>
        /// <param name="geoJson">String com o GeoJson.</param>
        /// <returns></returns>
        public static GeoAPI.Geometries.IGeometry ReadGeoJson(this string geoJson)
        {
            if (geoJson.IsEmpty() == true)
            {
                throw new Exception("GeoJson sem valores para converter em Geometria.");
            }

            try
            {
                JsonSerializer serializer = GeoJsonSerializer.CreateDefault();
                var geometry = serializer.Deserialize(new JsonTextReader(new StringReader(geoJson)), typeof(GeoAPI.Geometries.IGeometry));
                return (GeoAPI.Geometries.IGeometry)geometry;
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao tentar converter GeoJson em Geometria." + Environment.NewLine + ex.GetMessages());
            }
        }
    }
}
