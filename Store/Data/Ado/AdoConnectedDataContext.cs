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

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
            
        var query = "SELECT Id, Name, Description, Price, Type FROM Products";
        var command = new SqlCommand(query, connection);

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
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        var query = "INSERT INTO Products (Name, Description, Price, Type VALUES (@Name, @Description, @Price, @Type)";
        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Type", product.Type);

        return command.ExecuteNonQuery();
    }

    public int UpdateProduct(int id, Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = 
            "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, Type = @Type WHERE Id = @Id";
        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("Name", product.Name);
        command.Parameters.AddWithValue("Description", product.Description);
        command.Parameters.AddWithValue("Price", product.Price);
        command.Parameters.AddWithValue("Type", product.Type);
        command.Parameters.AddWithValue("Id", id);


        return command.ExecuteNonQuery();
    }

    public int DeleteProduct(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM Products WHERE Id = @Id";
        var command = new SqlCommand(query, connection);

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
        throw new NotImplementedException();
    }

    public int AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public int UpdateOrder(int id, Order order)
    {
        throw new NotImplementedException();
    }

    public int DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}