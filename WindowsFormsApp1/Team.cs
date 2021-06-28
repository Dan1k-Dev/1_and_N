using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } // название команды
        public string Coach { get; set; } // тренер

        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
