using System;

namespace TickTackToe
{
    public enum Player { None, X, O }

    public class GameEngine
    {
        public Player[,] Board { get; private set; } = new Player[3, 3];
        public Player CurrentPlayer { get; private set; } = Player.X;
        public int MoveCount { get; private set; } = 0;

        public void ResetGame()
        {
            Board = new Player[3, 3];
            MoveCount = 0;
        }

        public void SetStartingPlayer(Player player)
        {
            CurrentPlayer = player;
        }

        public void ChooseRandomStartingPlayer()
        {
            CurrentPlayer = new Random().Next(0, 2) == 0 ? Player.X : Player.O;
        }

        public bool MakeMove(int row, int col)
        {
            if (Board[row, col] != Player.None) return false;

            Board[row, col] = CurrentPlayer;
            MoveCount++;
            return true;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player.X) ? Player.O : Player.X;
        }

        public bool CheckWin(int row, int col)
        {
            Player p = Board[row, col];

            // Rea kontroll
            if (Board[row, 0] == p && Board[row, 1] == p && Board[row, 2] == p) return true;
            // Veeru kontroll
            if (Board[0, col] == p && Board[1, col] == p && Board[2, col] == p) return true;
            // Diagonaalid
            if (row == col && Board[0, 0] == p && Board[1, 1] == p && Board[2, 2] == p) return true;
            if (row + col == 2 && Board[0, 2] == p && Board[1, 1] == p && Board[2, 0] == p) return true;

            return false;
        }

        public bool CheckDraw()
        {
            return MoveCount == 9;
        }
    }
}
