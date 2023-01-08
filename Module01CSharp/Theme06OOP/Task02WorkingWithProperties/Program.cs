using System;

namespace Task02WorkingWithProperties
{
    internal class Program
    {
        static void Main()
        {
            Renderer drow = new Renderer();
            Player player = new Player(5, 2, '@');

            drow.DrawPlayer(player.PoxitionX, player.PoxitionY, player.Skin);

            Console.ReadKey();
        }
    }

    public class Player
    {
        public char Skin { get; private set; }
        public int PoxitionX { get; private set; }
        public int PoxitionY { get; private set; }

        public Player(int positionX, int positionY, char skin)
        {
            PoxitionX = positionX;
            PoxitionY = positionY;
            Skin = skin;
        }
    }

    public class Renderer
    {
        public void DrawPlayer(int x, int y, char playerSymbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(playerSymbol);
        }
    }
}