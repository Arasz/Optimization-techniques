namespace ConsoleApplication.Printer
{
    public interface IResultPrinter
    {
        IResultPrinter AddPrinter(IPrinter printer);

        /// <summary>
        /// Prints result with supplied printers 
        /// </summary>
        void Print(string content);
    }
}