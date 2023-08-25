using WebApplicationL5.Models;

namespace WebApplicationL5.Data;

public interface IDataContext
{
    public IList<Product> SelectProducts();
    public int AddProduct(Product product);
    public int UpdateProduct(Product product);
    public int DeleteProduct(int id);
    public int ProductsRowsCount();

    public IList<Customer> SelectCustomers();
    public int AddCustomer(Customer customer);
    public int UpdateCustomer(Customer customer);
    public int DeleteCustomer(int id);
    public int CustomersRowsCount();

    public IList<Order> SelectOrders();
    public int AddOrder(Order order);
    public int UpdateOrder(Order order);
    public int DeleteOrder(int id);
    public int OrdersRowsCount();

}