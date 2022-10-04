using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dapper;

using MVCAPI.Models;

namespace MVCAPI.Repository;

public class ProductRepository
{
    // private static IConfiguration Configuration;
    // static string connectionString = Configuration.GetConnectionString("default");
    // public ProductRepository(IConfiguration _configuration)
    // {
    //     Configuration = _configuration;
    // }

    // Making the sql connection with connection string
    static MySqlConnection con = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=db_aman");
    public static List<dynamic> GetData()
    {
        var users = con.Query<dynamic>("select *from productsrepo join images on productsrepo.id = images.imageId;").ToList();
        return users.ToList();
    }

    // Get the data with all the filters
    public static List<dynamic> GetData(string brand, string category, int priceFrom, int priceTo)
    {
        string filterQuery = "Select * from productsrepo join images on productsrepo.id = images.imageId WHERE "; // joining the products and images table so that data can be together
        if (brand != "") // checking if the brand exists in the products table
        {
            filterQuery = filterQuery + $"brand IN ({brand})";  
            if (category != "") // checking if the category exists in the products table
            {
                filterQuery = filterQuery + $" AND category IN ({category})"; 
                if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table
                {
                    filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}"; 
                }
            }
        }
        else if (category != "") // checking if the category exists in the products table if not brand given
        {
            filterQuery = filterQuery + $"category IN ({category})"; 
            if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table
            {
                filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}";
            }
        }
        else if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table if not brand and category given 
        {
            filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}";
        }
        else // if no parameters are available
        {
            filterQuery = "select *from productsrepo join images on productsrepo.id = images.imageId;";
        }
        var products = con.Query<dynamic>(filterQuery);
        return products.ToList(); // return the users list
    }


}
