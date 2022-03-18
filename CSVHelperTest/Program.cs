using CsvHelper;
using CSVHelperTest;
using System.Globalization;

using (var reader = new StreamReader(@".\test.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<CSVTest>();
    foreach (var record in records)
    {
        Console.WriteLine($"{record.Id},{record.Name}");
    }
}