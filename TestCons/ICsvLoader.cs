namespace TestCons;

public interface ICsvLoader
{
    List<TaxiRecord> LoadFromCsv(string path);
}