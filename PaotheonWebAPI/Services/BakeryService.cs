using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaotheonWebAPI.Models;
using PaotheonWebAPI.Contexts;
using PaotheonWebAPI.Interfaces;
using NetTopologySuite.Geometries;
using PaotheonWebAPI.Extensions;

namespace PaotheonWebAPI.Services
{
    public class BakeryService : IBakery
    {
        public DatabaseContext _context;

        public BakeryService()
        {
            _context = new DatabaseContext();
        }
        public List<Bakery> GetBakeries(int hour, double latitude, double longitude)
        {
            Point clientLocation = new Point(longitude, latitude)
            {
                SRID = 4326
            };

            double distance = 5000;

            return _context.Bakery.AsEnumerable().Where(b => b.location.ProjectTo(5530).Distance(clientLocation.ProjectTo(5530)) <= distance).Where(b => b.openHour <= hour).Where(b => b.closeHour > hour).ToList();
        }
        public Bakery GetBakery(int id)
        {
            Bakery bakery = _context.Bakery.Single(b => b.id == id);

            return bakery;
        }
        public void CreateBakery(Bakery bakery)
        {
            bakery.location = new Point(bakery.longitude, bakery.latitude)
            {
                SRID = 4326
            };
            _context.Bakery.Add(bakery);
            _context.SaveChanges();
        }
        public void UpdateBakery(int id, Bakery bakery)
        {
            Bakery bakeryFinded = _context.Bakery.Single(b => b.id == id);

            if (bakeryFinded != null)
            {
                _context.Attach(bakeryFinded);

                bakery.location = new Point(bakery.longitude, bakery.latitude)
                {
                    SRID = 4326
                };

                bakeryFinded.name = bakery.name;
                bakeryFinded.details = bakery.details;
                bakeryFinded.openHour = bakery.openHour;
                bakeryFinded.closeHour = bakery.closeHour;
                bakeryFinded.latitude = bakery.latitude;
                bakeryFinded.longitude = bakery.longitude;
                bakeryFinded.location = bakery.location;

                _context.Bakery.Update(bakeryFinded);
                _context.SaveChanges();
            }
        }
        public void DeleteBakery(int id)
        {
            Bakery bakeryFinded = _context.Bakery.Single(b => b.id == id);

            if (bakeryFinded != null)
            {
                _context.Bakery.Remove(bakeryFinded);
                _context.SaveChanges();
            }
        }
    }
}
