namespace TestCons;

public interface ICsvExporter
{
    public void SaveToCsv<T>(string path, List<T> records);
}