using SoccerApp.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp.Repository
{
    public class SoccerTeamRepository
    {
        private readonly List<SoccerTeam> _soccerTeamDatabase = new List<SoccerTeam>();

        private int _Count = 0;

        public bool CreateTeam(SoccerTeam soccerTeam)
        {
            if (soccerTeam is null)
            {
                return false;
            }
            _Count++;
            soccerTeam.ID = _Count;
            _soccerTeamDatabase.Add(soccerTeam);
            return true;
        }
        public IEnumerable<SoccerTeam> GetSoccerTeams()
        {
            return _soccerTeamDatabase;
        }
        public SoccerTeam GetSoccerTeamById(int id)
        {
            foreach (var team in _soccerTeamDatabase)
            {
                if (team.ID== id)
                {
                    return team;
                }
            }

            return null;
        }
        public bool UpdateSoccerTeam(int id, SoccerTeam newSoccerTeamData)
        {
            SoccerTeam oldTeamData = GetSoccerTeamById(id);
            if (oldTeamData is null)
            {
                return false;
            }
            oldTeamData.ID = id;
            oldTeamData.TeamName = newSoccerTeamData.TeamName;
            oldTeamData.TeamMembers = newSoccerTeamData.TeamMembers;

            return true;
        }
        public bool DeleteSoccerTeam(int id)
        {
            foreach (var team in _soccerTeamDatabase)
            {
                if (team.ID==id)
                {
                    _soccerTeamDatabase.Remove(team);
                    return true;
                }
            }
            return false;
        }
    }
}
