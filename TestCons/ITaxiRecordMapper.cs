namespace TestCons;

public interface ITaxiRecordMapper
{
    List<TaxiTrip> ToTaxiTrip(List<TaxiRecord> records);
}