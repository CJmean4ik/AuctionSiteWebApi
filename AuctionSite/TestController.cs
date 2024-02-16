using AuctionSite.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.API
{
    public class TestController : Controller
    {
        private readonly AuctionDbContext _db;

        public TestController(AuctionDbContext db)
        {
            _db = db;
        }

        [HttpGet("/lots")]
        public async Task<ActionResult> Get()
        {
            var result = await _db.Lots.ToListAsync();

            return Json(new { Lots = result });
        }
    }
}
