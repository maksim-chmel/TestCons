namespace TestCons;

public class DataCleaner : IDataCleaner
{
    public void CleanStoreAndFwdFlag(List<TaxiRecord> records)
    {
        foreach (var record in records)
        {
            if (!string.IsNullOrEmpty(record.store_and_fwd_flag))
            {
                record.store_and_fwd_flag = record.store_and_fwd_flag.Trim();

               
                if (record.store_and_fwd_flag.Equals("N", StringComparison.OrdinalIgnoreCase))
                    record.store_and_fwd_flag = "No";
                else if (record.store_and_fwd_flag.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    record.store_and_fwd_flag = "Yes";
                
            }
        }
        Console.WriteLine("Cleaning finished");
    }
}