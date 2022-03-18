using CsvHelper.Configuration.Attributes;

namespace CSVHelperTest
{
    public class CSVTest
    {
        [Name("id")]
        public string Id { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }
}
