namespace BattleShip.MainComponents.GameBoardLogic.BaseBattleOperations
{
    public class GameBoardUtilis
    {
        public static int BackToMenu { get; set; }

        public static int GetColumnNumber(char column)
        {
            var strColumn = (column).ToString();
            return int.Parse(strColumn) + 1;
        }

        public static int GetRowNumber(char letter)
        {
            var index = char.ToUpper(letter) - 64;
            return index;
        }
    }
}
