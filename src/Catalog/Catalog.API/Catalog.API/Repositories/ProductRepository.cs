using Catalog.API.Data.interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _context.Products.Find(product => product.Category == categoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Find(product => product.Name == name).ToListAsync();
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var removed = await _context.Products.DeleteOneAsync(x => x.Id == id);
            return removed.IsAcknowledged && removed.DeletedCount > 0;
        }

        public async Task<bool> Update(Product product)
        {
            var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updated.IsAcknowledged && updated.ModifiedCount > 0;
        }
    }
}
