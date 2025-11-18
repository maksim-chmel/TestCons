namespace TestCons;

public class DatabaseService : IDatabaseService
{
    public void SaveTrips(List<TaxiTrip> trips)
    {
       ConverTime(trips);
        using (var db = new TaxiDbContext())
        {
            db.TaxiTrips.AddRange(trips);
            db.SaveChanges();
        } 
    }

    private void ConverTime(List<TaxiTrip> trips)
    {
        TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        foreach (var trip in trips)
        {
            if (trip.tpep_pickup_datetime.HasValue)
                trip.tpep_pickup_datetime = TimeZoneInfo.ConvertTimeToUtc(trip.tpep_pickup_datetime.Value, est);

            if (trip.tpep_dropoff_datetime.HasValue)
                trip.tpep_dropoff_datetime = TimeZoneInfo.ConvertTimeToUtc(trip.tpep_dropoff_datetime.Value, est);
        }
    }
    
}