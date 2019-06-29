using BrainPowerApp.Model;
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
                context.players.Add(new Player { name = "name", score = 10, recordDate = "13/06/2019", avatarId = 0 });
                context.players.Add(new Player { name = "name2", score = 10, recordDate = "13/06/2019", avatarId = 1 });
                context.players.Add(new Player { name = "name", score = 10, recordDate = "13/06/2019", avatarId = 2 });
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
            Player temp = await context.players.FindAsync(id);
            if (temp == null)
            {
                return NotFound();
            }
            return temp;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Player>> Post([FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.players.Add(player);
            await context.SaveChangesAsync();
            return CreatedAtAction("get", player);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Player>> Put(int id, [FromBody] Player player)
        {
            if (id != player.id)
            {
                return BadRequest("Ids dont match");
            }
            context.Entry(player).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(player);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> Delete(int id)
        {
            Player temp = await context.players.FindAsync(id);
            if (temp == null)
            {
                return NotFound("Player not found");
            }
            context.players.Remove(temp);
            await context.SaveChangesAsync();
            return Ok(temp);
        }
    }
}
