using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp.POCOs
{
    public class SoccerTeam
    {
        public int ID { get; set; }
        public string TeamName { get; set; }
        public List<Player> TeamMembers { get; set; } = new List<Player>();

        public SoccerTeam()
        {

        }

        public SoccerTeam(string teamName,List<Player>teamMembers)
        {
            TeamName = teamName;
            TeamMembers = teamMembers;
        }
    }
}
