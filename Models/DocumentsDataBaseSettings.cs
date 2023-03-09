namespace MongoTest.Models
{
    public class DocumentsDataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DocsCollectionName { get; set; } = null!;
    }
}
