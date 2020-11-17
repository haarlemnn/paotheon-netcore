using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaotheonWebAPI.Models;

namespace PaotheonWebAPI.Interfaces
{
    public interface IBakery
    {
        public List<Bakery> GetBakeries(int hour, double latitude, double longitude);
        public Bakery GetBakery(int id);
        public void CreateBakery(Bakery bakery);
        public void UpdateBakery(int id, Bakery bakery);
        public void DeleteBakery(int id);
    }
}
