//---Display.cs


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MM_project;
using System.Threading.Tasks;

namespace MM_project
{
    internal class Display
    {


        public static void DisplayMenu()

        {
            //Shows MainMenu 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nMenu");
            Console.WriteLine("\n====");
            Console.WriteLine();
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Show content of account");
            Console.WriteLine("3. New expense/income");
            Console.WriteLine("4. Account (edit / remove etc)");

            Console.WriteLine("5. Read from file");
            Console.WriteLine("6. Save to file");
            Console.WriteLine("7. Work using file not local memory edit (edit / remove etc)");
            Console.WriteLine("q. Save snd Quit");
            Console.Write("Enter your choice: ");
            Console.ResetColor();

        }

    }
}



