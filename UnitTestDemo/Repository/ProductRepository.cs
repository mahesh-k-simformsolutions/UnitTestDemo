using Database;
using Database.Tables;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Repository
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<int> Add(Product product);
        Task<int> Update(Product product);
    }
    public class ProductRepository : IProductRepository
    {
        private AppDbConext _db;
        public ProductRepository(AppDbConext db)
        {
            _db = db;   
        }

        public async Task<int> Add(Product product)
        {
            _db.Products.Add(product);  
            return await _db.SaveChangesAsync();    
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x =>x.Id == id);   
        }

        public async Task<int> Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            return await _db.SaveChangesAsync();
        }
    }
}
