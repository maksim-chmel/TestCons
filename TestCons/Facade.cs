namespace TestCons;

public class Facade(
    ICsvLoader loader,
    IDataCleaner cleaner,
    IDuplicateFinder duplicateFinder,
    IDatabaseService service,
    ITaxiRecordMapper mapper,
    ICsvExporter exporter,
    IDataValidator validator)
{
    public void Run(string pathToCSV, string pathToDuplication)
    {
        
        var records = loader.LoadFromCsv(pathToCSV);
        
        var validList = validator.ValidateRecords(records);
        
        cleaner.CleanStoreAndFwdFlag(validList);
        
        var duplicates = duplicateFinder.FindDuplicates(validList);
        
        exporter.SaveToCsv(pathToDuplication, duplicates);
        
        var cleanList = duplicateFinder.RemoveDuplicates(validList);
        
        var trips = mapper.ToTaxiTrip(cleanList);
        
        service.SaveTrips(trips);
    }
}