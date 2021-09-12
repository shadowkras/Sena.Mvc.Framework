using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using Sena.Mvc.Framework.Core.Extensions;
using ProjNet.CoordinateSystems.Transformations;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Sena.Mvc.Framework.Data.Extensions
{
    /// <summary>
    /// Extension class to work with geometries and geoJSON.
    /// </summary>
    public static class IGeometryExtensions
    {
        /// <summary>
        /// Converts a geometry to GeoJSON.
        /// </summary>
        /// <param name="geometry">IGeometry object.</param>
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
        /// Converts a GeoJSON into an object of IGeometry type.
        /// </summary>
        /// <param name="geoJson">GeoJSON string.</param>
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

        /// <summary>
        /// Returns the area of a geometry in square meters.
        /// </summary>
        /// <param name="geometry">Objeto of IGeometry type.</param>
        /// <returns></returns>
        public static double GetAreaSquareMeters(this GeoAPI.Geometries.IGeometry geometry)
        {
            // ver http://epsg.io/3857 (código utilizado pelo google, bing, arcgis e esri).
            //const string epsg3857 = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\", DATUM[\"D_WGS_1984\", SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Mercator_Auxiliary_Sphere\"], PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0]]";
            //const string epsg3857 = "PROJCS[\"WGS 84 / Pseudo - Mercator\",GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.0174532925199433,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]],PROJECTION[\"Mercator_1SP\"],PARAMETER[\"central_meridian\",0],PARAMETER[\"scale_factor\",1],PARAMETER[\"false_easting\",0],PARAMETER[\"false_northing\",0],UNIT[\"metre\",1,AUTHORITY[\"EPSG\",\"9001\"]],AXIS[\"X\",EAST],AXIS[\"Y\",NORTH],EXTENSION[\"PROJ4\",\" + proj = merc + a = 6378137 + b = 6378137 + lat_ts = 0.0 + lon_0 = 0.0 + x_0 = 0.0 + y_0 = 0 + k = 1.0 + units = m + nadgrids = @null + wktext + no_defs\"],AUTHORITY[\"EPSG\",\"3857\"]]";

            var ctFactory = new CoordinateTransformationFactory();
            var csWgs84 = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;

            try
            {
                //TODO Revisar os formatos utilizados
                var epsg3857 = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator.WKT;
                var cs3857 = new ProjNet.CoordinateSystems.CoordinateSystemFactory().CreateFromWkt(epsg3857);

                var coordinateTransformation = ctFactory.CreateFromCoordinateSystems(csWgs84, cs3857);

                var mathTransform = coordinateTransformation.MathTransform;

                var newCoordinates = new List<Coordinate>();
                foreach (var ponto in geometry.Coordinates)
                {
                    var transformedCoord = mathTransform.Transform(ponto.X, ponto.Y, ponto.Z);
                    var newCoord = new Coordinate(transformedCoord.o1, transformedCoord.o2, transformedCoord.o3);
                    newCoordinates.Add(newCoord);
                }

                var points = newCoordinates;

                var calculatedArea = System.Math.Abs(points.Take(points.Count - 1)
                   .Select((p, i) => (points[i + 1].X - p.X) * (points[i + 1].Y + p.Y))
                   .Sum() / 2);

                return calculatedArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
