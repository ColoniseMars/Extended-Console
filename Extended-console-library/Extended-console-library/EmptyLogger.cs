using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_console_library
{
    public class EmptyLogger : CommandLineLogger
    {
        public void LogRead(string UserInput) { }

        public void LogReadLine(string UserInput) { }

        public void LogWrite(string UserInput) { }

        public void LogWriteLine(string UserInput) { }
    }
}
