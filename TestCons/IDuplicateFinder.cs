namespace TestCons;

public interface IDuplicateFinder
{
    List<TaxiRecord> FindDuplicates(List<TaxiRecord> records);
    List<TaxiRecord> RemoveDuplicates(List<TaxiRecord> records);
}