using NetTopologySuite.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaotheonWebAPI.Extensions
{
    static class GeometryExtensions
    {
        static readonly CoordinateSystemServices _coordinateSystemServices
       = new CoordinateSystemServices(
           new Dictionary<int, string>
           {
                // Coordinate systems:

                [4326] = GeographicCoordinateSystem.WGS84.WKT,

                // This coordinate system covers the area of our data.
                // Different data requires a different coordinate system.
                [5530] =
               @"
                    PROJCS[""SAD69(96) / Brazil Polyconic"",
                        GEOGCS[""SAD69(96)"",
                            DATUM[""South_American_Datum_1969_96"",
                                SPHEROID[""GRS 1967 Modified"",6378160,298.25,
                                    AUTHORITY[""EPSG"",""7050""]],
                                TOWGS84[-67.35,3.88,-38.22,0,0,0,0],
                                AUTHORITY[""EPSG"",""1075""]],
                            PRIMEM[""Greenwich"",0,
                                AUTHORITY[""EPSG"",""8901""]],
                            UNIT[""degree"",0.0174532925199433,
                                AUTHORITY[""EPSG"",""9122""]],
                            AUTHORITY[""EPSG"",""5527""]],
                        PROJECTION[""Polyconic""],
                        PARAMETER[""latitude_of_origin"",0],
                        PARAMETER[""central_meridian"",-54],
                        PARAMETER[""false_easting"",5000000],
                        PARAMETER[""false_northing"",10000000],
                        UNIT[""metre"",1,
                            AUTHORITY[""EPSG"",""9001""]],
                        AXIS[""X"",EAST],
                        AXIS[""Y"",NORTH],
                        AUTHORITY[""EPSG"",""5530""]]
                "
           });
        public static Geometry ProjectTo(this Geometry geometry, int srid)
        {
            var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

            var result = geometry.Copy();
            result.Apply(new MathTransformFilter(transformation.MathTransform));

            return result;
        }

        class MathTransformFilter : ICoordinateSequenceFilter
        {
            readonly MathTransform _transform;

            public MathTransformFilter(MathTransform transform)
                => _transform = transform;

            public bool Done => false;
            public bool GeometryChanged => true;

            public void Filter(CoordinateSequence seq, int i)
            {
                var x = seq.GetX(i);
                var y = seq.GetY(i);
                var z = seq.GetZ(i);
                _transform.Transform(ref x, ref y, ref z);
                seq.SetX(i, x);
                seq.SetY(i, y);
                seq.SetZ(i, z);
            }
        }
    }
}
