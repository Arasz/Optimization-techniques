using System;

namespace ConsoleApplication.Printer
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string content) => Console.WriteLine(content);
    }
}