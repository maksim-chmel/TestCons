namespace TestCons;

public class DataValidator : IDataValidator
{
    public List<TaxiRecord> ValidateRecords(List<TaxiRecord> records)
    {
        var validRecords = records
            .Where(r =>
                r.tpep_pickup_datetime != default(DateTime) &&
                r.tpep_dropoff_datetime != default(DateTime) &&
                r.tpep_dropoff_datetime >= r.tpep_pickup_datetime &&
                (!r.passenger_count.HasValue || r.passenger_count.Value >= 0) &&
                (!r.trip_distance.HasValue || r.trip_distance.Value >= 0) &&
                (!r.fare_amount.HasValue || r.fare_amount.Value >= 0) &&
                (!r.tip_amount.HasValue || r.tip_amount.Value >= 0) &&
                (!r.PULocationID.HasValue || r.PULocationID.Value >= 0) &&
                (!r.DOLocationID.HasValue || r.DOLocationID.Value >= 0)
            )
            .ToList();

        Console.WriteLine($"Валидация завершена. Валидных записей: {validRecords.Count}");
        return validRecords;
    }
}