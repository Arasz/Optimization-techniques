using System.IO;

namespace ConsoleApplication.Printer
{
    public class FilePrinter : IPrinter
    {
        private readonly FileInfo _file;

        public FilePrinter(string directory, string fileName)
        {
            _file = new FileInfo(Path.Combine(string.IsNullOrEmpty(directory) ?
                Directory.GetCurrentDirectory() : directory, fileName));
            if(_file.Exists)
                _file.Delete();
        }

        public void Print(string content)
        {
            using (var stream = _file.Open(FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
            using (var fileWriter = new StreamWriter(stream))
            {
                fileWriter.Write(content);
            }
        }
    }
}