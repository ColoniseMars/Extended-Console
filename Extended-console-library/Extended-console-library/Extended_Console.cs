using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extended_console_library
{
    public static class Extended_Console
    {
        private static CommandLineLogger Logger = new EmptyLogger();
        public static void SetLogger(CommandLineLogger logger)
        {
            Logger = logger;
        }
        
        private static Thread InputThread;

        ///<summary>
        ///Starts the console reading thread.
        ///</summary>
        public static void Start()
        {
            if(InputThread != null)
            {
                WriteLine("Console thread already running.");
            }
            else
            {
                InputThread = new Thread(InputHandler);
                InputThread.Start();
            }
        }

        ///<summary>
        ///Begins stopping of console reading thread.
        ///</summary>
        public static void Stop()
        {
            KeepGoing = false;
            WriteLine("Please press enter to exit...", false);
        }

        private static bool KeepGoing = true;
        private static List<string> enteredLines = new List<string>(); //the lines that have yet to be read.
        private static List<char> currentInput = new List<char>(); //the characters currently entered by the user, yet to be submitted
        private static int yPositionCursor, xPositionCursor;

        private static void InputHandler() //the function that handles the user input
        {
            ConsoleKeyInfo enteredKey;
            char enteredChar;
            int charIntValue;

            while (KeepGoing)
            {
                SaveCurrentCursorPosition();

                enteredKey = Console.ReadKey();
                enteredChar = enteredKey.KeyChar;
                charIntValue = (int)enteredChar;
                if(charIntValue <= 31 || (charIntValue >= 127 && charIntValue <= 159))
                { //control keys
                    Console.SetCursorPosition(xPositionCursor, yPositionCursor); //cancels any act of the control key

                    switch (enteredChar)
                    {
                        case '\b':
                            if(currentInput.Count > 0)
                            {
                                currentInput.RemoveAt(currentInput.Count - 1);
                                Console.Write("\b \b");
                            }
                            break;

                        case '\r':
                        case '\n':
                            currentInput.Add((char)13);
                            currentInput.Add((char)10);
                            enteredLines.Add(new string(currentInput.ToArray())); //converts the list to a char array, then to a new string
                            currentInput.Clear();
                            Console.Write("\n");
                            break;
                            
                        default:
                            break;
                    }
                }
                else //characters
                {
                    currentInput.Add(enteredChar);
                }
            }


        }

        private static void SaveCurrentCursorPosition() //saves the cursor position
        {
            yPositionCursor = Console.CursorTop;
            xPositionCursor = Console.CursorLeft;
        }

        ///<summary>
        ///Writes a string to the console with a newline, takes a boolean to decide if to log or not.
        ///</summary>
        public static void WriteLine(string text, bool DoLog) //performs the function of WriteLine
        {
            for(int i = 0; i<currentInput.Count; i++)
            {
                Console.Write("\b \b"); //move back one spot, clear it, move back one spot
            }
            if (DoLog)
            {
                Logger.LogWriteLine(text);
            }
            Console.WriteLine(text);
            Console.Write(new string(currentInput.ToArray()));
            SaveCurrentCursorPosition();
        }

        ///<summary>
        ///Writes a string to the console with a newline and logs it.
        ///</summary>
        public static void WriteLine(string text)
        {
            WriteLine(text, true);
        }

        ///<summary>
        ///Writes a string to the console, takes a boolean to decide if to log or not.
        ///</summary>
        public static void Write(string text, bool DoLog) //performs the function of Write
        {
            for (int i = 0; i < currentInput.Count; i++)
            {
                Console.Write("\b \b"); //move back one spot, clear it, move back one spot
            }
            if (DoLog)
            {
                Logger.LogWrite(text);
            }
            Console.Write(text);
            Console.Write(new string(currentInput.ToArray()));
            SaveCurrentCursorPosition();
        }

        ///<summary>
        ///Writes a string to the console and logs it.
        ///</summary>
        public static void Write(string text)
        {
            Write(text, true);
        }

        ///<summary>
        ///Reads the next character, takes a bool to decide if to log or not.
        ///</summary>
        public static int Read(bool DoLog) //performs the function of Read
        {
            while(enteredLines.Count == 0)
            {
                //wait for there to be a line
            }
            int toReturn = enteredLines[0][0];
            if(enteredLines[0].Length > 1) //cuts off the first char of the line
            {
                enteredLines[0] = enteredLines[0].Substring(1);
            }
            else
            {
                enteredLines.RemoveAt(0); //there is only one char in the string, remove string.
            }
            if (DoLog)
            {
                Logger.LogRead(toReturn.ToString());
            }
            return toReturn;
        }

        ///<summary>
        ///Reads the next character and logs.
        ///</summary>
        public static int Read()
        {
            return Read(true);
        }


        ///<summary>
        ///Reads a whole line without the newline characters and returns a string, takes a bool to decide if to log or not.
        ///</summary>
        public static string ReadLine(bool DoLog) //performs the function of ReadLine
        {
            while (enteredLines.Count == 0)
            {
                //waiting until there is something
            }
            string toReturn = enteredLines[0];
            enteredLines.RemoveAt(0);
            bool finished = false;
            while (toReturn.Length > 0 && !finished)
            {
                if (toReturn[toReturn.Length - 1] == '\r' || toReturn[toReturn.Length - 1] == '\n') //cuts off the newline characters
                {
                    toReturn = toReturn.Remove(toReturn.Length - 1);
                }
                else
                {
                    finished = true;
                }
            }
            if (DoLog)
            {
                Logger.LogReadLine(toReturn);
            }
            return toReturn;
        }

        ///<summary>
        ///Reads a whole line without the newline characters, returns a string and logs.
        ///</summary>
        public static string ReadLine()
        {
            return ReadLine(true);
        }

    }
}
