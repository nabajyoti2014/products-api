using System.Data.SqlClient;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService
    {
        private SqlConnection GetConnection()
        {

            string connectionString = "Server=tcp:productdbsrv.database.windows.net,1433;Initial Catalog=productdb;user id=nabajyoti;password=Password@123;";
            return new SqlConnection(connectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = sqlDataReader.GetInt32(0),
                        ProductName = sqlDataReader.GetString(1),
                        Quantity = sqlDataReader.GetInt32(2)
                    };

                    productList.Add(_product);
                }
            }
            sqlConnection.Close();
            return productList;
        }


        public Product GetProduct(string _productId)
        {
            int productId = int.Parse(_productId);
            string statement = String.Format("SELECT ProductID,ProductName,Quantity from Products WHERE ProductID={0}", productId);
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);
            Product product = new Product();
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                sqlDataReader.Read();
                product.ProductID = sqlDataReader.GetInt32(0);
                product.ProductName = sqlDataReader.GetString(1);
                product.Quantity = sqlDataReader.GetInt32(2);
                return product;
            }
        }

        public int AddProduct(Product product)
        {
            string statement = String.Format("INSERT INTO Products(ProductID,ProductName,Quantity) VALUES({0},'{1}',{2})", product.ProductID, product.ProductName, product.Quantity);
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }
    }

}
