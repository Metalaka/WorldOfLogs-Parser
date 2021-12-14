namespace FileImporter.Parsers
{
    using Domain;

    public interface IParser
    {
        public PageData[] ParseFiles(string[] files);
    }
}
