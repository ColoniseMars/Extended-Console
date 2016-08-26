using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_console_library
{
    public interface CommandLineLogger
    {
        void LogReadLine(string UserInput);
        void LogWriteLine(string UserInput);
        void LogWrite(string UserInput);
        void LogRead(string UserInput);
    }
}
