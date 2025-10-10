using MongoDB.Driver;

namespace Quiz.Infrastructure.Data.DbContexts
{
    public class MongoDbContext
    {
        private readonly ILogger<MongoDbContext> logger;
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase database;

        public MongoDbContext(ILogger<MongoDbContext> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

            string? connectionString = configuration.GetConnectionString("MongoDbConnection");
            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mongoClient = new MongoClient(mongoUrl);
            database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => database;
    }
}
