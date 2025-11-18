using System.Globalization;

namespace TestCons;

public class DuplicateFinder : IDuplicateFinder
{
    public List<TaxiRecord> FindDuplicates(List<TaxiRecord> records)
    {
        return records
            .GroupBy(r => new { r.tpep_pickup_datetime, r.tpep_dropoff_datetime, r.passenger_count })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.Skip(1))
            .ToList();
        
    }
    

    public List<TaxiRecord> RemoveDuplicates(List<TaxiRecord> records)
    {
        return records
            .GroupBy(r => new { r.tpep_pickup_datetime, r.tpep_dropoff_datetime, r.passenger_count })
            .Select(g => g.First())
            .ToList();
    }
}