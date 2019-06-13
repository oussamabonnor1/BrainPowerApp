using leaderboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leaderboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        PlayerContext context;
        public LeaderBoardController(PlayerContext c)
        {
            context = c;
            if (context.players.Count() == 0)
            {
                context.players.Add(new Player { name = "name", score = 10, recordDate = "13/06/2019" });
                context.players.Add(new Player { name = "name2", score = 10, recordDate = "13/06/2019" });
                context.players.Add(new Player { name = "name", score = 10, recordDate = "13/06/2019" });
            }
            context.SaveChanges();
        }

        // GET api/leaderboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await context.players.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            return await context.players.FindAsync(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
