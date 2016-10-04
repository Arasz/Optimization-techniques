namespace ConsoleApplication.Data
{
    public class DataLoader : IDataLoader
    {
        private readonly string _dataPath;

        private IData _data;

        public IData Data { get; }

        public DataLoader(string dataPath)
        {
            _dataPath = dataPath;
        }

        private void Load()
        {
        }
    }
}