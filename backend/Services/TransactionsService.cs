using mywebapp.api.Models;
using MongoDB.Driver;

namespace mywebapp.api.Services
{
    public class TransactionsService
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public TransactionsService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _transactions = database.GetCollection<Transaction>(config["MongoDbSettings:CollectionName"]);
        }

        public async Task<List<Transaction>> GetAsync() =>
            await _transactions.Find(t => true).ToListAsync();

        public async Task<Transaction> GetAsync(string id) =>
            await _transactions.Find(t => t.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Transaction transaction) =>
            await _transactions.InsertOneAsync(transaction);

        public async Task UpdateAsync(string id, Transaction transaction) =>
            await _transactions.ReplaceOneAsync(t => t.Id == id, transaction);

        public async Task DeleteAsync(string id) =>
            await _transactions.DeleteOneAsync(t => t.Id == id);
    }
}
