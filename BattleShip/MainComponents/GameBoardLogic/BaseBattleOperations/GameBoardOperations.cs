using System;
using System.Text.RegularExpressions;
using BattleShip.MainComponents.GameBoardLogic.BaseBattleOperations;

namespace BattleShip.MainComponents.BattleLogic.ShotAction
{
    public class GameBoardOperations : BaseBattleOperations
    {
        public Grid BoatLayer { get; set; }

        public override ConsoleColor Foreground { get; set; }

        public override Grid PlayerLayer { get; set; }

        protected override bool isInputValid(string shot)
        {
            shot = shot.Replace(" ", "");
            if (shot == "" || shot == " " || shot == null)
            {
                return false;
            }
            if (shot.ToLower() == "exit")
            {
               BattleShip.Menu.exit();
            }
            var pattern = @"^[A-ja-j]{1}[0-9]{1}$";
            var reg = new Regex(pattern);
            var match = reg.Match(shot);
            if (match.Value != shot)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void WriteShot(string shot)
        {
            var col = GameBoardUtilis.GetColumnNumber(shot[1]);
            var row = GameBoardUtilis.GetRowNumber(shot[0]);
            bool hit = isShotHit(shot);
            if (hit)
            {
                this.PlayerLayer.Matrix[row, col] = 'x';
                Boat hitBoat = HitBoat(shot);
            }
            else
            {
                this.PlayerLayer.Matrix[row, col] = ' ';
            }
        }

        private bool IsBoatSunk(Boat hitBoat)
        {
            if (hitBoat.Hits >= hitBoat.Length)
            {
                hitBoat.Sunk = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("You sunk the " + hitBoat.Name + "!");
                Console.ForegroundColor = Foreground;
                return true;
            }
            return false;
        }

        private Boat HitBoat(string shot)
        {
            int col = GameBoardUtilis.GetColumnNumber(shot[1]);
            int row = GameBoardUtilis.GetRowNumber(shot[0]);
            Boat hitBoat;
            foreach (Boat boat in BoatLayer.Boats)
            {
                hitBoat = boat;
                var rowMatch = false;
                for (int i = 0; i < boat.Length; i++)
                {
                    for (int ii = 0; ii < 2; ii++)
                    {
                        if (ii == 0 && boat.Coordinates[i, ii] == row)
                        {
                            rowMatch = true;
                        }
                        if (ii == 1 &&
                            rowMatch &&
                            boat.Coordinates[i, ii] == col)
                        {
                            hitBoat.Hits++;
                            IsBoatSunk(hitBoat);
                            return hitBoat;
                        }
                    }
                }
            }
            throw new ArgumentException("No hit boat");
        }

        private bool isShotHit(string shot)
        {
            if (BoatLayer.Matrix[GameBoardUtilis.GetRowNumber(shot[0]),
                GameBoardUtilis.GetColumnNumber(shot[1])] != BoatLayer.Symbol)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(shot + " HIT!" + "\n\n");
                Console.ForegroundColor = Foreground;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(shot + " MISSED!" + "\n\n");
            Console.ForegroundColor = Foreground;
            return false;
        }
    }
}
