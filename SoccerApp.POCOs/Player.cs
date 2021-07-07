using SoccerApp.POCOs.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp.POCOs
{
    public class Player
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}"; 
            }
        }

        //enum that will represent the position of the  player
        public PlayerPosition PlayerPosition { get; set; }

        public Player()
        {

        }

        public Player(string firstName,string lastName, PlayerPosition playerPosition)
        {
            FirstName = firstName;
            LastName = lastName;
            PlayerPosition = playerPosition;
        }

    }
}
