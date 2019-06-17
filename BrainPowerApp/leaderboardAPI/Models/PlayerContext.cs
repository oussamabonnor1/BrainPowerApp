using Microsoft.EntityFrameworkCore;
using BrainPowerApp.Model;

namespace leaderboardAPI.Models{
    public class PlayerContext : DbContext{
        public DbSet<Player> players {get; set;}

        public PlayerContext(DbContextOptions<PlayerContext> options) : base (options)
        {

        }

    }
}