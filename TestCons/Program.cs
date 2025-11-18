using DotNetEnv;
using TestCons;

internal class Program
{
   public static string path = "/Users/maksim/RiderProjects/TestCons/TestCons/.env";
    static void Main(string[] args)
    {
        
        Env.Load(path);
        Console.WriteLine($"Current dir: {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"CSV_PATH = '{Environment.GetEnvironmentVariable("CSV_PATH")}'");
        Console.WriteLine($"DUPLICATES_PATH = '{Environment.GetEnvironmentVariable("DUPLICATES_PATH")}'");
        string csvPath = Environment.GetEnvironmentVariable("CSV_PATH");
        string duplicatesPath = Environment.GetEnvironmentVariable("DUPLICATES_PATH");
        ICsvLoader loader = new CsvLoader();
        IDataCleaner cleaner = new DataCleaner();
        IDuplicateFinder duplicateFinder = new DuplicateFinder();
        IDatabaseService service = new DatabaseService();
        ITaxiRecordMapper mapper = new TaxiRecordMapper();
        ICsvExporter exporter = new CsvExporter();
        IDataValidator validator = new DataValidator();

        var facade = new Facade(loader, cleaner, duplicateFinder, service, mapper, exporter , validator);

        facade.Run(csvPath, duplicatesPath);
        Console.WriteLine("ETL process finished.");
    }
}

