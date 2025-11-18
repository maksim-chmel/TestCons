using System.Globalization;

namespace TestCons;

public class CsvExporter: ICsvExporter
{
    public void SaveToCsv<T>(string path, List<T> records)
    {
        using var writer = new StreamWriter(path);
        using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(records);
    }
    
}