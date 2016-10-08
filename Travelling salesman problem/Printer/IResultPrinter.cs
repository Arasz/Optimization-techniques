using ConsoleApplication.Printer.ContentBuilders;

namespace ConsoleApplication.Printer
{
    public interface IResultPrinter
    {
        IResultPrinter AddPrinter(IPrinter printer, IContentBuilder contentBuilder);

        /// <summary>
        /// Prints result with supplied printers 
        /// </summary>
        void Print(string title);
    }
}