using MVCDatabaseApp.Models;

namespace MVCDatabaseApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
