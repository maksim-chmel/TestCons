namespace TestCons;

public class TaxiRecordMapper : ITaxiRecordMapper
{
    private TaxiTrip ToTaxiTrip(TaxiRecord record)
    {
        return new TaxiTrip
        {
            tpep_pickup_datetime = record.tpep_pickup_datetime,
            tpep_dropoff_datetime = record.tpep_dropoff_datetime,
            passenger_count = record.passenger_count ?? 0,
            trip_distance = record.trip_distance ?? 0,
            store_and_fwd_flag = record.store_and_fwd_flag,
            PULocationID = record.PULocationID ?? 0,
            DOLocationID = record.DOLocationID ?? 0,
            fare_amount = record.fare_amount ?? 0,
            tip_amount = record.tip_amount ?? 0
        };
    }
    public List<TaxiTrip> ToTaxiTrip(List<TaxiRecord> records)
    {
        return records.Select(ToTaxiTrip).ToList();
    }
}