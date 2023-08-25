using Microsoft.EntityFrameworkCore;
using WebApplicationL5.Models;

namespace WebApplicationL5.Data.EF;

public class EFDataContext : DbContext, IDataContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public IList<Product> SelectProducts()
    {
        throw new NotImplementedException();
    }

    public int AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public int UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public int DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public int ProductsRowsCount()
    {
        throw new NotImplementedException();
    }


    public IList<Customer> SelectCustomers()
    {
        return Customers.ToList();
    }

    public int AddCustomer(Customer customer)
    {
        Customers.Add(customer);

        return SaveChanges();
    }

    public int UpdateCustomer(Customer customer)
    {
        var foundCustomer = Customers.Find(customer.Id);
        if (foundCustomer == null) return 0;

        foundCustomer.FirstName = customer.FirstName;
        foundCustomer.LastName = customer.LastName;
        foundCustomer.Age = customer.Age;
        foundCustomer.Country = customer.Country;

        return SaveChanges();
    }

    public int DeleteCustomer(int id)
    {
        var foundCustomer = Customers.FirstOrDefault(row => row.Id == id);

        if (foundCustomer != null) Customers.Remove(foundCustomer);

        return SaveChanges();
    }

    public int CustomersRowsCount()
    {
        throw new NotImplementedException();
    }

    public int GetCustomerId()
    {
        var maxCustomerId = Customers.Max(customer => customer.Id);
        return maxCustomerId;
    }


    public IList<Order> SelectOrders()
    {
        return Orders.ToList();
    }

    public int AddOrder(Order order)
    {
        Orders.Add(order);

        return SaveChanges();
    }

    public int UpdateOrder(Order order)
    {
        var foundOrder = Orders.Find(order.Id);
        if (foundOrder == null) return 0;

        foundOrder.CustomerId = order.CustomerId;
        foundOrder.ProductId = order.ProductId;
        foundOrder.Count = order.Count;
        foundOrder.CreatedAt = order.CreatedAt;

        return SaveChanges();
    }

    public int DeleteOrder(int id)
    {
        var foundOrder = Orders.FirstOrDefault(order => order.Id == id);

        if (foundOrder != null) Orders.Remove(foundOrder);

        return SaveChanges();
    }

    public int OrdersRowsCount()
    {
        throw new NotImplementedException();
    }

    public int GetOrderId()
    {
        var maxOrderId = Orders.Max(order => order.Id);
        return maxOrderId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Customer>().Property(p => p.FirstName).HasColumnName("first_name");
        modelBuilder.Entity<Customer>().Property(p => p.LastName).HasColumnName("last_name");

        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<Order>().Property(p => p.CustomerId).HasColumnName("customer_id");
        modelBuilder.Entity<Order>().Property(p => p.ProductId).HasColumnName("product_id");
        modelBuilder.Entity<Order>().Property(p => p.CreatedAt).HasColumnName("created_at");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Datasource=localhost;Database=usersdb;User=root;Password=3079718;");
    }
}