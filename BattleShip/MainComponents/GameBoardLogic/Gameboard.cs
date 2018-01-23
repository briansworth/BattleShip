using BattleShip.MainComponents.BattleLogic.ShotAction;
using System;

namespace BattleShip
{
    public sealed class Gameboard 
    {
        public Grid BoatLayer { get { return BaseBattleOperations.BoatLayer; } set { BaseBattleOperations.BoatLayer = value; } }
        public Grid PlayerLayer { get { return BaseBattleOperations.PlayerLayer; } set { BaseBattleOperations.PlayerLayer = value; } }
        public int Rockets { get { return BaseBattleOperations.Rockets; } set { BaseBattleOperations.Rockets = value; } }
        public ConsoleColor Foreground {get { return BaseBattleOperations.Foreground; } set { BaseBattleOperations.Foreground = value; } }
        private ConsoleColor Background { get; set; }
        private ConsoleColor GameBoardForeGround { get; set; }

        private GameBoardOperations BaseBattleOperations { get; set; }

        public void Shoot() { this.BaseBattleOperations.Shoot(); }

        public Gameboard(Grid boatGrid, Grid playerGrid)
        {
            const int rockets = 10;
            BoatLayer = boatGrid;
            PlayerLayer = playerGrid;

            BaseBattleOperations = new GameBoardOperations()
            {
                Rockets = rockets,
                BoatLayer = BoatLayer

            };

            GameBoardForeGround = ConsoleColor.White;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }

        public Gameboard()
        {
            const int rockets = 40;

            BaseBattleOperations = new GameBoardOperations()
            {
                Rockets = rockets,
            };

            var playerGrid = new Grid();
            PlayerLayer = playerGrid;
            var boatGrid = new Grid(10, 10, ' ');
            var boatList = Boat.GenerateBoats();
            Boat.PlaceBoats(boatList, boatGrid);
            BoatLayer = boatGrid;
            Foreground = ConsoleColor.Gray;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
    }
}
