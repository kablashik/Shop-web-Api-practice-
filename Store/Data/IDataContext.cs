using WebApplicationL5.Models;

namespace WebApplicationL5.Data;

public interface IDataContext
{
    public IList<Product> SelectProducts();
    public int AddProduct(Product product);
    public int UpdateProduct(int id, Product product);
    public int DeleteProduct(int id);

    public IList<Customer> SelectCustomers();
    public int AddCustomer(Customer customer);
    public int UpdateCustomer(int id, Customer customer);
    public int DeleteCustomer(int id);

    public IList<Order> SelectOrders();
    public int AddOrder(Order order);
    public int UpdateOrder(int id, Order order);
    public int DeleteOrder(int id);

}