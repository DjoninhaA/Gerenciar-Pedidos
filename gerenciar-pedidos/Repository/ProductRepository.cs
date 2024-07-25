using gerenciar_pedidos.Models;
using Microsoft.EntityFrameworkCore;


public class ProductRepository : IProductRepository {

    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Product> Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProduct()
    {
        return await _context.Products.ToListAsync();
    }
}