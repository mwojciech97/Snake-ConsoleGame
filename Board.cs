using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Board
    {
        private int width;
        private int height;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public Board()
        {
            width = 20;
            height = 20;
        }
        public void CreateBoard()
        {
            for(int i = 1; i <= width; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 1; i <= width; i++)
            {
                Console.SetCursorPosition(i, height);
                Console.Write("-");
            }
            for (int i = 1; i <= height; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }
            for (int i = 1; i <= height; i++)
            {
                Console.SetCursorPosition(width + 1, i);
                Console.Write("|");
            }
        }
    }
}