using System.IO;

namespace ConsoleApplication.Printer
{
    public class FilePrinter : IPrinter
    {
        private FileInfo file;

        public FilePrinter(string fileName)
        {
            file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), fileName));
            if(file.Exists)
                file.Delete();
        }

        public void Print(string content)
        {
            using (var stream = file.Open(FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
            using (var fileWriter = new StreamWriter(stream))
            {
                fileWriter.Write(content);
            }
        }
    }
}