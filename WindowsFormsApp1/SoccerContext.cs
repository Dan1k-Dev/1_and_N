using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SoccerContext : DbContext
    {
    public SoccerContext()
        : base("DefaultConnection")
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

