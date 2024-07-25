using gerenciar_pedidos.Models;


public interface IProductRepository {

    Task<IEnumerable<Product>> GetAllProduct(); 
    //Task<Product> GetByIdAsync(int id);      
    Task<Product> Create(Product product);
    //Task UpdateAsync(Product product);        
    //Task DeleteAsync(int id);       

    }