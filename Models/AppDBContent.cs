using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await Products.ToListAsync();
        }

        public async Task AddProduct(Product product)
        {
            var existingProduct = await FindByName(product.Name);
            if (existingProduct == null)
            {
                Products.Add(product);
            }
            else
            {
                existingProduct.Count += product.Count;
                existingProduct.Price = product.Price;
                Products.Update(existingProduct);
            }
            SaveChanges();
        }

        public async Task<Product?> FindByName(string name)
        {
            return await Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<Product>> FindByPrice(double price)
        {
            return await Products
                                 .Where(p => p.Price == price)
                                 .ToListAsync();
        }

        public async Task DeleteProduct(string name)
        {
            Product? product = await FindByName(name);
            if (product != null)
            {
                Products.Remove(product);
                SaveChanges();
            }
        }

        public async Task ClearDB()
        {
            var products = await Products.ToListAsync();
            Products.RemoveRange(products);
            SaveChanges();
        }
    }
}