using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
