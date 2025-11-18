using System.Globalization;
using CsvHelper;

namespace TestCons;

public class CsvLoader : ICsvLoader
{
    public List<TaxiRecord> LoadFromCsv(string path)
    {
        var records = new List<TaxiRecord>();
        Console.WriteLine("ETL process started.");

        var reader = new StreamReader("/Users/maksim/RiderProjects/TestCons/TestCons/sample-cab-data.csv");
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TaxiRecordMap>();
        records = csv.GetRecords<TaxiRecord>().ToList();
        Console.WriteLine($"Loaded {records.Count} records.");
        return records;
    }
        
}