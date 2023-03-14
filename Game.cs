using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        #region vairables
        #region BoardSize
        private int boardWidth;
        private int boardHeight;
        #endregion
        #region SnakeLengthAndLocation
        int[] X = new int[50];
        int[] Y = new int[50];
        int parts = 3;
        #endregion
        #region FruitLocation
        int fruitX;
        int fruitY;
        #endregion
        #region KeyReading
        ConsoleKey consoleKey = new ConsoleKey();
        ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();
        #endregion
        private int score = 0;
        private bool lost = false;
        Random rnd = new Random();
        #endregion
        public bool Lost
        {
            get
            {
                return lost;
            }
            private set
            {
                lost = value;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            private set
            {
                score = value;
            }
        }
        public int BoardWidth
        {
            get { return boardWidth; }
        }
        public int BoardHeight
        {
            get { return boardHeight; }
        }
        public Game()
        {
            this.boardWidth = 20;
            this.boardHeight = 20;
        }
        public Game(int boardWidth, int boardHeight)
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
        }
        public void DrawFruit()
        {
            
            Console.SetCursorPosition(fruitX, fruitY);
            Console.Write("*");
            Thread.Sleep(100);
        }
        private void ChangeSnakeDirection()
        {
            int endX = X[parts - 1];
            int endY = Y[parts - 1];
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
            Console.SetCursorPosition(endX, endY);
            Console.Write(" ");
        }
        private void DrawSnake()
        {
            for (int i = 0; i < parts; i++)
            {
                Console.SetCursorPosition(X[i], Y[i]);
                Console.Write("#");
            }
        }
        private void CheckBite()
        {
            for(int i = 1; i < parts; i++)
            {
                if (X[0] == X[i] && Y[0] == Y[i]) Lost = true;
            }
        }
        public void PlayerInput()
        {
            if (Console.KeyAvailable)
            {
                consoleKeyInfo = Console.ReadKey(true);
                consoleKey = consoleKeyInfo.Key;
            }
        }
        private void MoveSnake()
        {
            switch (consoleKey)
            {
                case ConsoleKey.W:
                    if (Y[0] - Y[1] == 1) Lost = true;
                    else
                    {
                        ChangeSnakeDirection();
                        Y[0]--;
                    } 
                    break;
                case ConsoleKey.A:
                    if (X[0] - X[1] == 1) Lost = true;
                    else
                    {
                        ChangeSnakeDirection();
                        X[0]--;
                    }
                    break;
                case ConsoleKey.S:
                    if (Y[0] - Y[1] == -1) Lost = true;
                    else
                    {
                        ChangeSnakeDirection();
                        Y[0]++;
                    }
                    break;
                case ConsoleKey.D:
                    if (X[0] - X[1] == -1) Lost = true;
                    else
                    {
                        ChangeSnakeDirection();
                        X[0]++;
                    }
                    break;
                default:
                    break;
            }
            DrawSnake();
            CheckBite();
        }
        public void GameLogic()
        {
            DrawFruit();
            MoveSnake();
            
            if (X[0] < 2 || 
                X[0] > BoardWidth - 1|| 
                Y[0] < 2 ||
                Y[0] > BoardHeight - 1) Lost = true;

            if (X[0] == fruitX && Y[0] == fruitY)
            {
                score += 10;
                parts++;

                while (X.Contains(fruitX) && Y.Contains(fruitY))
                {
                    fruitX = rnd.Next(2, BoardWidth - 1);
                    fruitY = rnd.Next(2, BoardHeight - 1);
                }
            }
        }
        public void SetStartingPositions()
        {
            X[0] = 10;
            Y[0] = 10;
            X[1] = 10;
            Y[1] = 11;
            X[2] = 10;
            Y[2] = 12;
            fruitX = rnd.Next(2, BoardWidth - 1);
            fruitY = rnd.Next(2, BoardHeight - 1);
        }
    }
}