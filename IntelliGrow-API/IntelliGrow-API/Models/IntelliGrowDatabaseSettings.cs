namespace IntelliGrow_API.Models
{
    public class IntelliGrowDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DevicesCollectionName { get; set; } = null!;

        public string HistoryCollectionName { get; set; } = null!;
    }
}
