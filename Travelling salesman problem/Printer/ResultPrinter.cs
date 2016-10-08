using System.Collections.Generic;

namespace ConsoleApplication.Printer
{
    public class ResultPrinter : IResultPrinter
    {
        private IList<IPrinter> _printers = new List<IPrinter>();

        public IResultPrinter AddPrinter(IPrinter printer)
        {
            _printers.Add(printer);
            return this;
        }

        public void Print(string content)
        {
            foreach (var printer in _printers)
                printer.Print(content);
        }
    }
}