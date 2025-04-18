using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp84.UI
{
    // This class is basically for putting both the program text and also the user inputted text in the centre basically manging program display
    public static class ConsoleUI
    {
        // This method puts text in the middle of console
        public static void PutTextInMiddle(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = Math.Max((windowWidth - text.Length) / 2, 0);
            Console.WriteLine(new string(' ', padding) + text);
        }

        // This method clears current console line 
        public static void DeleteStuffFromConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, currentLineCursor);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


        // This method basically puts anything the user inputted into the middle of the console
        public static string PutUserInputCenter(string prompt)
        {

            PutTextInMiddle(prompt);


            int inputLine = Console.CursorTop;

            Console.SetCursorPosition(0, inputLine);
            DeleteStuffFromConsoleLine();

            string result = string.Empty;
            while (true)
            {

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);


                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (result.Length > 0)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                }

                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    result += keyInfo.KeyChar;
                }


                Console.SetCursorPosition(0, inputLine);
                DeleteStuffFromConsoleLine();

                int padding = Math.Max((Console.WindowWidth - result.Length) / 2, 0);
                Console.SetCursorPosition(padding, inputLine);
                Console.Write(result);
            }


            Console.WriteLine();
            return result;
        }

        // This method clears the entire console screen 
        public static void FreshPage()
        {
            Console.Clear();
        }

        // This method basically stops the program from executing until the user has inpuuted a random key
        public static void ProgramHold()
        {
            PutTextInMiddle("\uD83D\uDCF1 Enter any key to proceed... \uD83D\uDCF1");
            Console.ReadKey(true);
        }
    }
}
