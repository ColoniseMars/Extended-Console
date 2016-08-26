using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extended_console_library;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Extended_Console.Start();

            Extended_Console.SetLogger(new DemoLogger()); //here you can enter your own logger instead of the default empty one.
            //Not entering a logger will not negatively impact the extended console.

            int character;
            while (true)
            {
                character = Extended_Console.Read();
                string wholeline = Extended_Console.ReadLine();
                string test = ""; //put breakpoint here if you want to.

                if(character == 'X')
                {
                    Extended_Console.WriteLine("Writing line in 2 seconds");
                    Thread.Sleep(2000);
                    Extended_Console.WriteLine("Written a line");
                }
                if(character == 'x')
                {
                    Extended_Console.WriteLine("Writing in 2 seconds");
                    Thread.Sleep(2000);
                    Extended_Console.Write("Written line");
                }
            }
        }
    }

    class DemoLogger : CommandLineLogger
    {
        public void LogRead(string UserInput) //logs Read
        {
            //Here you can implement various ways to log the activity to a file or something else
            //All the names of these functions correspond to the functions found in the extended console
        }

        public void LogReadLine(string UserInput) //logs ReadLine
        {
            //for exaple, you could log: [01-01-16 23:45] User Entered: "Line the user wrote"
        }

        public void LogWriteLine(string UserInput) //logs WriteLine
        {
            //and here you could write to the log what you just wrote into the console.
        }

        public void LogWrite(string UserInput) //logs Write
        {
            //Same here, but for the Write function
        }

    }
}
