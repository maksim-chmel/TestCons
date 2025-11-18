namespace TestCons;

public interface IDataValidator
{
    List<TaxiRecord> ValidateRecords(List<TaxiRecord> records);
}