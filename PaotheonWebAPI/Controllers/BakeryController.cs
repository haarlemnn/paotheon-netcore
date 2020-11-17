using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaotheonWebAPI.Models;
using PaotheonWebAPI.Interfaces;

namespace PaotheonWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BakeryController : ControllerBase
    {
        private ILogger _logger;
        private IBakery _ibakery;

        public BakeryController(ILogger<BakeryController> logger, IBakery ibakery)
        {
            _logger = logger;
            _ibakery = ibakery;
        }

        [HttpGet("/bakeries")]
        public ActionResult<List<Bakery>> GetBakeries(int hour, double latitude, double longitude)
        {
            return _ibakery.GetBakeries(hour, latitude, longitude);
        }
        [HttpGet("/bakeries/{id}")]
        public ActionResult<Bakery> GetBakery(int id)
        {
            return _ibakery.GetBakery(id);
        }
        [HttpPost("/bakeries")]
        public ActionResult<Bakery> CreateBakery(Bakery bakery)
        {
            _ibakery.CreateBakery(bakery);
            return bakery;
        }
        [HttpPut("/bakeries/{id}")]
        public ActionResult<Bakery> UpdateBakery(int id, Bakery bakery)
        {
            _ibakery.UpdateBakery(id, bakery);
            return bakery;
        }
        [HttpDelete("/bakeries/{id}")]
        public ActionResult<int> DeleteBakery(int id)
        {
            _ibakery.DeleteBakery(id);
            return id;
        }
    }
}
