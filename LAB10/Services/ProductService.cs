using System.Threading.Tasks;
using System.Linq;
using LAB10.Data;
using LAB10.DTO;
using LAB10.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB10.Services
{
    public class ProductService
    {
        private readonly ShoppingCartContext _context;

        public ProductService(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Weight = productDto.Weight,
                Width = productDto.Width,
                Height = productDto.Height,
                Depth = productDto.Depth,
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            foreach (var categoryId in productDto.CategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    ProductId = product.ProductId,
                    CategoryId = categoryId
                };

                _context.ProductCategories.Add(productCategory);
            }

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
