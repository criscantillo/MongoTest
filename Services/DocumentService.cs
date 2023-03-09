using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoTest.Models;

namespace MongoTest.Services
{
    public class DocumentService
    {
        private readonly IMongoCollection<Document> _docsCollection;


        public DocumentService(IOptions<DocumentsDataBaseSettings> documentsDataBaseSettings)
        {
            var mongoClient = new MongoClient(documentsDataBaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient
                .GetDatabase(documentsDataBaseSettings.Value.DatabaseName);

            _docsCollection = mongoDB
                .GetCollection<Document>(documentsDataBaseSettings.Value.DocsCollectionName);
        }

        public async Task<List<Document>> GetAsync() =>
        await _docsCollection.Find(_ => true).ToListAsync();

        public async Task<Document?> GetAsync(string id) =>
            await _docsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Document newDoc) =>
            await _docsCollection.InsertOneAsync(newDoc);

        public async Task UpdateAsync(string id, Document modDoc) =>
            await _docsCollection.ReplaceOneAsync(x => x.Id == id, modDoc);

        public async Task RemoveAsync(string id) =>
            await _docsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
