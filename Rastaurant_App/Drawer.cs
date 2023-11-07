using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
    public class Drawer
    {
        // method for drawing menu and returing selected option
        public static void DrawMenu(string title, string[] items, int selectedIndex)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Draw the title
            int titleX = (windowWidth - title.Length) / 2;
            Console.SetCursorPosition(titleX, 2);
            Console.WriteLine(title);

            // Draw the menu options
            for (int i = 0; i < items.Length; i++)
            {
                int optionX = (windowWidth - items[i].Length) / 2;
                int optionY = 7 + i;

                Console.SetCursorPosition(optionX, optionY);

                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(items[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(items[i]);
                }
            }

            // Draw the navigation ionstructions
            Console.SetCursorPosition(6, windowHeight - 4);
            Console.WriteLine(@"To navigate through menu use arrow keys up - ↑, and down - ↓.
        Confirm selection by pressing 'enter'.");

            // Draw the borders
            for (int i = 0; i < windowWidth; i++)
            {
                Console.SetCursorPosition(i, 4);
                Console.Write("─");
                Console.SetCursorPosition(i, windowHeight - 2);
                Console.Write("─");
                Console.SetCursorPosition(i, windowHeight - 5);
                Console.Write("─");
            }
            for (int i = 5; i < windowHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(windowWidth - 1, i);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 4);
            Console.Write("┌");
            Console.SetCursorPosition(windowWidth - 1, 4);
            Console.Write("┐");
            Console.SetCursorPosition(0, windowHeight - 2);
            Console.Write("└");
            Console.SetCursorPosition(0, windowHeight - 5);
            Console.Write("├");
            Console.SetCursorPosition(windowWidth - 1, windowHeight - 2);
            Console.Write("┘");
            Console.SetCursorPosition(windowWidth - 1, windowHeight - 5);
            Console.Write("┤");
            Console.SetCursorPosition(0, 0);
        }
    }
}
