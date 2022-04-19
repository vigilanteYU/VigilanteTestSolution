using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MongoDBTest
{
    public class MongoDbApp : ConsoleAppBase
    {
        private readonly ILogger<MongoDbApp> _logger;
        private readonly MongoDbConfig _config;
        private readonly IMongoCollection<Book> _bookCollection;

        public MongoDbApp(ILogger<MongoDbApp> logger,
            IOptions<MongoDbConfig> options)
        {
            _logger = logger;
            _config = options.Value;
            var mongoClient = new MongoClient(_config.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_config.DatabaseName);
            _bookCollection = mongoDatabase.GetCollection<Book>(_config.BooksCollectionName);
        }

        [Command("get-all-items")]
        public async Task GetItemsAsync()
        {
            var books = await _bookCollection.Find(_ => true).ToListAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            foreach (var book in books)
            {
                var jsonstr = JsonSerializer.Serialize(book, jsonOptions);
                _logger.LogInformation(jsonstr);
            }
        }

        [Command("insert-item")]
        public async Task CreateItemAsync(string bookname, string author, string category, decimal price)
        {
            var book = new Book
            {
                BookName = bookname,
                Author = author,
                Category = category,
                Price = price,
            };
            await _bookCollection.InsertOneAsync(book);
            _logger.LogInformation("登録しました。");
        }
    }
}
