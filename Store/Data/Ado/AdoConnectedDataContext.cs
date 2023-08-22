using WebApplicationL5.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Metadata;
using MySql.Data.MySqlClient;

namespace WebApplicationL5.Data;

public class AdoConnectedDataContext : IDataContext
{
    private readonly string? _connectionString;

    public AdoConnectedDataContext(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public IList<Product> SelectProducts()
    {
        var products = new List<Product>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
            
        var query = "SELECT Id, Name, Description, Price, Type FROM Products";
        var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var product = new Product()
            {
                Id = (int)reader["Id"],
                Name = reader["Name"] as string,
                Description = reader["Description"] as string,
                Price = (double)reader["Price"],
                Type = (ProductType)Enum.Parse(typeof(ProductType), reader["Type"].ToString())
            };
                    
            products.Add(product);
        }

        return products;
    }

    public int AddProduct(Product product)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        
        var query = "INSERT INTO Products (Name, Description, Price, Type) VALUES (@Name, @Description, @Price, @Type)";
        var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Type", product.Type);

        return command.ExecuteNonQuery();
    }

    public int UpdateProduct(int id, Product product)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = 
            "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, Type = @Type WHERE Id = @Id";
        var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Name", product.Name);
        command.Parameters.AddWithValue("Description", product.Description);
        command.Parameters.AddWithValue("Price", product.Price);
        command.Parameters.AddWithValue("Type", product.Type);
        command.Parameters.AddWithValue("Id", id);


        return command.ExecuteNonQuery();
    }

    public int DeleteProduct(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM Products WHERE Id = @Id";
        var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Id", id);

        return command.ExecuteNonQuery();
    }

    public IList<Customer> SelectCustomers()
    {
        var customers = new List<Customer>();

        using var connection = new MySqlConnection(_connectionString);
        
        connection.Open();

        var query = "SELECT id, first_name, last_name, age, country FROM Customer";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var customer = new Customer
            {
                Id = (int)reader["id"],
                FirstName = reader["first_name"] as string,
                LastName = reader["last_name"] as string,
                Age = (int)reader["age"],
                Country = reader["country"] as string
            };
            
            customers.Add(customer);
        }

        return customers;
    }

    public int AddCustomer(Customer customer)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "INSERT INTO Customer (first_name, last_name, age, country) VALUES (@FirstName, @LastName, @Age, @Country)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("FirstName", customer.FirstName);
        command.Parameters.AddWithValue("LastName", customer.LastName);
        command.Parameters.AddWithValue("Age", customer.Age);
        command.Parameters.AddWithValue("Country", customer.Country);

        return command.ExecuteNonQuery();
    }

    public int UpdateCustomer(int id, Customer customer)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "UPDATE Customer SET first_name = @FirstName, last_name = @LastName, age = @Age, country = @Country WHERE id = @Id";
        using var command = new MySqlCommand(query, connection);
        
        command.Parameters.AddWithValue("FirstName", customer.FirstName);
        command.Parameters.AddWithValue("LastName", customer.LastName);
        command.Parameters.AddWithValue("Age", customer.Age);
        command.Parameters.AddWithValue("Country", customer.Country);
        command.Parameters.AddWithValue("Id", customer.Id);

        return command.ExecuteNonQuery();
    }

    public int DeleteCustomer(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM customer WHERE id = @Id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Id", id);

        return command.ExecuteNonQuery();
    }

    public IList<Order> SelectOrders()
    {
        var orders = new List<Order>();
        using var connection = new MySqlConnection(_connectionString);
        
        connection.Open();
        var query = "SELECT * FROM orders";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var order = new Order()
            {
                Id = (int)reader["id"],
                CustomerId = (int)reader["customer_id"],
                ProductId = (int)reader["product_id"],
                Count = (int)reader["count"],
                CreatedAt = (DateTime)reader["created_at"]
            };
            
            orders.Add(order);
        }

        return orders;
    }

    public int AddOrder(Order order)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query = "INSERT INTO orders (customer_id, product_id, count, created_at) VALUES (@CustomerId, @ProductId, @Count, @CreatedAt)";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("CustomerId", order.CustomerId);
        command.Parameters.AddWithValue("ProductId", order.ProductId);
        command.Parameters.AddWithValue("Count", order.Count);
        command.Parameters.AddWithValue("CreatedAt", order.CreatedAt);

        return command.ExecuteNonQuery();
    }

    public int UpdateOrder(int id, Order order)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query = "UPDATE ORDERS SET customer_id = @CustomerId, product_id = @ProductId, count = @Count, created_at = @CreatedAt WHERE id = @Id";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("CustomerId", order.CustomerId);
        command.Parameters.AddWithValue("ProductId", order.ProductId);
        command.Parameters.AddWithValue("Count", order.Count);
        command.Parameters.AddWithValue("CreatedAt", order.CreatedAt);

        return command.ExecuteNonQuery();
    }

    public int DeleteOrder(int id)
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM orders WHERE id = @Id";
        
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("Id", id);

        return command.ExecuteNonQuery();
    }
}