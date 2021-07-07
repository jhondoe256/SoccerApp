using SoccerApp.POCOs;
using SoccerApp.POCOs.Positions;
using SoccerApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerAppUI
{
    public class Program_UI
    {
        //Ui will make calls to the 'back end' .... 
        //we need to referece objects that represent player and teams (2 repositories)
        private readonly PlayerRepository _playerRepo = new PlayerRepository();
        private readonly SoccerTeamRepository _soccerTeamRepo = new SoccerTeamRepository();


        public void Run()
        {
            Seed();
            RunApplication();
        }

        private void RunApplication()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Welcome to the Soccer App\n" +
                              "1. Add a Player\n" +
                              "2. View All Players\n" +
                              "3. View a single Player\n" +
                              "4. Update a Player\n" +
                              "5. Delete a Player\n" +
                              "---------------------\n" +
                              "6. Add A Team\n" +
                              "7. View all Teams\n" +
                              "8. view single Team\n" +
                              "9. Update Team\n" +
                              "10.Delete Team\n" +
                              "50.Close Application\n");

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        AddAPlayer();
                        break;
                    case "2":
                        ViewAllPlayers();
                        break;
                    case "3":
                        ViewSinglePlayer();
                        break;
                    case "4":
                        UpdatePlayer();
                        break;
                    case "5":
                        DeletePlayer();
                        break;
                    case "6":
                        AddTeam();
                        break;
                    case "7":
                        ViewAllTeams();
                        break;
                    case "8":
                        ViewSingleTeam();
                        break;
                    case "9":
                        UpdateTeam();
                        break;
                    case "10":
                        DeleteTeam();
                        break;
                    case "50":
                        isRunning = false;
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid opperation");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
        }

        private void DeleteTeam()
        {
            Console.Clear();
            Console.WriteLine("Please input TeamID:");
            var userInputTeamID = int.Parse(Console.ReadLine());

            var success = _soccerTeamRepo.DeleteSoccerTeam(userInputTeamID);
            if (success == true)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILURE");
            }

            Console.ReadKey();
        }

        private void UpdateTeam()
        {
            Console.Clear();
            Console.WriteLine("Please input TeamID:");
            var userInputTeamID = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Please Enter a team name:");
            var userInputTeamName = Console.ReadLine();

            bool hasFilledPositions = false;

            List<Player> players = _playerRepo.GetPlayers().ToList();

            List<Player> PlayersForASpecificTeam = new List<Player>();
            while (hasFilledPositions == false)
            {
                Console.WriteLine("Do you have any team members to add? y/n");
                var userInputTeamMembersToAdd = Console.ReadLine();

                if (userInputTeamMembersToAdd == "Y".ToLower())
                {
                    foreach (var player in players)
                    {
                        DisplayPlayerData(player);
                    }

                    Console.WriteLine("Please input Player Id for transfer:");
                    int userInputPlayerId = int.Parse(Console.ReadLine());

                    Player playerFromDatabase = _playerRepo.GetPlayerByID(userInputPlayerId);

                    PlayersForASpecificTeam.Add(playerFromDatabase);

                }
                else
                {
                    hasFilledPositions = true;
                }
            }

            SoccerTeam newSoccerTeamData = new SoccerTeam(userInputTeamName, PlayersForASpecificTeam);

            var success = _soccerTeamRepo.UpdateSoccerTeam(userInputTeamID, newSoccerTeamData);

            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILURE");
            }


            Console.ReadKey();

        }

        private void ViewSingleTeam()
        {
            Console.Clear();
            Console.WriteLine("Please input TeamID:");
            var userInputTeamID = int.Parse(Console.ReadLine());

            SoccerTeam team = _soccerTeamRepo.GetSoccerTeamById(userInputTeamID);

            if (team is null)
            {
                Console.WriteLine("FAILURE");
            }
            else
            {
                DisplayTeamData(team);
            }

            Console.ReadLine();
        }

        private void ViewAllTeams()
        {
            Console.Clear();
            List<SoccerTeam> soccerTeamsFromDatabase = _soccerTeamRepo.GetSoccerTeams().ToList();

            foreach (var team in soccerTeamsFromDatabase)
            {
                DisplayTeamData(team);
            }

            Console.ReadKey();
        }

        private void DisplayTeamData(SoccerTeam soccerTeam)
        {
            Console.WriteLine($"Team ID: {soccerTeam.ID}\n" +
                              $"Team Name: {soccerTeam.TeamName}\n" +
                              $"---------------------------------\n");

            foreach (var player in soccerTeam.TeamMembers)
            {
                DisplayPlayerData(player);
            }
        }

        private void AddTeam()
        {
            Console.Clear();
            Console.WriteLine("Please Enter a team name:");
            var userInputTeamName = Console.ReadLine();

            bool hasFilledPositions = false;

            List<Player> players = _playerRepo.GetPlayers().ToList();

            List<Player> PlayersForASpecificTeam = new List<Player>();
            while (hasFilledPositions==false)
            {
                Console.WriteLine("Do you have any team members to add? y/n");
                var userInputTeamMembersToAdd = Console.ReadLine();

                if (userInputTeamMembersToAdd == "Y".ToLower())
                {
                    foreach (var player in players)
                    {
                        DisplayPlayerData(player);
                    }

                    Console.WriteLine("Please input Player Id for transfer:");
                    int userInputPlayerId = int.Parse(Console.ReadLine());

                    Player playerFromDatabase = _playerRepo.GetPlayerByID(userInputPlayerId);

                    PlayersForASpecificTeam.Add(playerFromDatabase);
                    
                }
                else
                {
                    hasFilledPositions = true;
                }
            }

            SoccerTeam team = new SoccerTeam(userInputTeamName,PlayersForASpecificTeam);

            var success = _soccerTeamRepo.CreateTeam(team);

            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILURE");
            }

            Console.ReadKey();
        }

        private void ViewAllPlayers()
        {
            Console.Clear();
            List<Player> playersInDatabase = _playerRepo.GetPlayers().ToList();

            foreach (var player in playersInDatabase)
            {
                DisplayPlayerData(player);
            }


            Console.ReadKey();
        }

        private void DisplayPlayerData(Player player)
        {
            Console.WriteLine(
                              $"player Id {player.ID}\n" +
                              $"Player Name {player.FullName}\n" +
                              $"player pos {player.PlayerPosition}\n");

            Console.WriteLine("--------------------------------------------------");
        }

        private void UpdatePlayer()
        {
            Console.Clear();
            Console.WriteLine("Please input a PlayerID:");
            var userInputPlayerId = int.Parse(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Please input First Name:");
            var userInputFirstName = Console.ReadLine();

            Console.WriteLine("Please input Last Name:");
            var userInputLastName = Console.ReadLine();

            Console.WriteLine("Please Pick a Player Position:\n" +
                               "1.Goal Keeper\n" +
                               "2.Defender\n" +
                               "3.Centreback\n" +
                               "4.Sweeper\n" +
                               "5.Fullback\n" +
                               "6.Wingback\n" +
                               "7.Midfielder\n" +
                               "8.Central MidFielder\n" +
                               "9.Defensive MidFielder\n" +
                               "10.Deep Lying Playmaker\n" +
                               "11.Attacking MidFielder\n" +
                               "12.Winger\n" +
                               "13.Striker\n" +
                               "14.Centre Foward\n" +
                               "15.False 9\n");

            var userInputPos = int.Parse(Console.ReadLine());

            var PositionCoversion = (PlayerPosition)userInputPos;

            var newPlayerData = new Player(userInputFirstName, userInputLastName, PositionCoversion);

            bool success = _playerRepo.UpdatePlayer(userInputPlayerId, newPlayerData);

            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILURE");
            }

        }

        private void ViewSinglePlayer()
        {
            Console.Clear();
            Console.WriteLine("Please input a PlayerID:");
            var userInputPlayerId = int.Parse(Console.ReadLine());

            Player selectedPlayer = _playerRepo.GetPlayerByID(userInputPlayerId);

            if (selectedPlayer is null)
            {
                Console.WriteLine("The Player doesnt EXIST!");
            }
            else
            {
                DisplayPlayerData(selectedPlayer);
            }

            Console.ReadKey();
        }

        private void DeletePlayer()
        {
            Console.Clear();
            Console.WriteLine("Please input a PlayerID:");
            var userInputPlayerId = int.Parse(Console.ReadLine());

            bool success = _playerRepo.DeletePlayer(userInputPlayerId);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILURE");
            }
        }

        private void AddAPlayer()
        {
            Console.Clear();

            Console.WriteLine("Please input First Name:");
            var userInputFirstName = Console.ReadLine();

            Console.WriteLine("Please input Last Name:");
            var userInputLastName = Console.ReadLine();

            Console.WriteLine("Please Pick a Player Position:\n" +
                               "1.Goal Keeper\n" +
                               "2.Defender\n" +
                               "3.Centreback\n" +
                               "4.Sweeper\n" +
                               "5.Fullback\n" +
                               "6.Wingback\n" +
                               "7.Midfielder\n" +
                               "8.Central MidFielder\n" +
                               "9.Defensive MidFielder\n" +
                               "10.Deep Lying Playmaker\n" +
                               "11.Attacking MidFielder\n" +
                               "12.Winger\n" +
                               "13.Striker\n" +
                               "14.Centre Foward\n" +
                               "15.False 9\n");

            var userInputPos = int.Parse(Console.ReadLine());

            var PositionCoversion = (PlayerPosition)userInputPos;

            var player = new Player(userInputFirstName, userInputLastName, PositionCoversion);

            bool success = _playerRepo.AddPlayer(player);

            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILED");
            }

            Console.ReadKey();
        }

        private void Seed()
        {
            var playerA = new Player("Terry", "Brown",PlayerPosition.Attacking_Midfielder);
            var playerB = new Player("Johnny", "Dept",PlayerPosition.Goal_Keeper);
            var playerC = new Player("Joe", "Schmoe",PlayerPosition.Defender);
            var playerD = new Player("Bill", "Burr",PlayerPosition.Central_Midfielder);

            _playerRepo.AddPlayer(playerA);
            _playerRepo.AddPlayer(playerB);
            _playerRepo.AddPlayer(playerC);
            _playerRepo.AddPlayer(playerD);


            var TeamA = new SoccerTeam("AUGGIES", new List<Player> { playerA, playerC });
            var TeamB = new SoccerTeam("BOILERMAKERS", new List<Player> { playerD, playerB });

            _soccerTeamRepo.CreateTeam(TeamB);
            _soccerTeamRepo.CreateTeam(TeamA);
        }
    }
}
