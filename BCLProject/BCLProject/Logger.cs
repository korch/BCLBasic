using System;
using BCLProject.Interfaces;

namespace BCLProject
{
    class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
