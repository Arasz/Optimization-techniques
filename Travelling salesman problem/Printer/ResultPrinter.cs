using ConsoleApplication.Printer.ContentBuilders;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Printer
{
    public class ResultPrinter : IResultPrinter
    {
        private IList<IContentBuilder> _contentBuilders = new List<IContentBuilder>();
        private IList<IPrinter> _printers = new List<IPrinter>();

        public IResultPrinter AddPrinter(IPrinter printer, IContentBuilder contentBuilder)
        {
            _contentBuilders.Add(contentBuilder);
            _printers.Add(printer);
            return this;
        }

        public void Print(string title)
        {
            foreach (var printer in _printers.Zip(_contentBuilders, (printer, builder) => new { Printer = printer, ContentBuilder = builder }))
                printer.Printer.Print(printer.ContentBuilder.BuildContent(title));
        }
    }
}