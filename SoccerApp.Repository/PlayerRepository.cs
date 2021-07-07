using SoccerApp.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp.Repository
{
    public class PlayerRepository
    {
        //make a 'fake database' to hold all of the data
        private readonly List<Player> _playerDatabase = new List<Player>();
        
        //auto increments the id -> just like a real database
        private int _Count = 0;

        public bool AddPlayer(Player player) 
        {
            if (player is null)
            {
                return false;
            }

            _Count++;
            player.ID = _Count;
            _playerDatabase.Add(player);
            return true;
        }
        public IEnumerable<Player> GetPlayers()
        {
            return _playerDatabase;
        }
        public Player GetPlayerByID(int id)
        {
            foreach (var player in _playerDatabase)
            {
                if (player.ID==id)
                {
                    return player;
                }
            }

            return null;

        }
        public bool UpdatePlayer(int id, Player newPlayerData)
        {
            Player oldPlayerData = GetPlayerByID(id);

            if (oldPlayerData is null)
            {
                return false;
            }
            else
            {
                oldPlayerData.ID = id;
                oldPlayerData.FirstName = newPlayerData.FirstName;
                oldPlayerData.LastName = newPlayerData.LastName;
                oldPlayerData.PlayerPosition = newPlayerData.PlayerPosition;

                return true;
            }
        }
        public bool DeletePlayer(int id)
        {
            foreach (var player in _playerDatabase)
            {
                if (player.ID==id)
                {
                    _playerDatabase.Remove(player);
                    return true;
                }
            }

            return false;
        }
        
    }
}
