using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using NetTopologySuite.Geometries;

namespace PaotheonWebAPI.Models
{
    public class Bakery
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int openHour { get; set; }
        public int closeHour { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Point location { get; set; }
    }
}
