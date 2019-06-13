using Microsoft.EntityFrameworkCore;

namespace leaderboardAPI.Models{
    public class PlayerContext : DbContext{
        public DbSet<Player> players {get; set;}

        public PlayerContext(DbContextOptions<PlayerContext> options) : base (options)
        {

        }

    }
}