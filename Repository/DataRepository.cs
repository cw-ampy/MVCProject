using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dapper;
using MVCAPI.BusinessLogic;

using MVCAPI.Models;

namespace MVCAPI.Repository;

public class ProductRepository
{
    // public static List<dynamic> GetData(string connectionString)
    // {
    //     MySqlConnection con = new MySqlConnection(connectionString);
    //     var users = con.Query<dynamic>("select *from productsrepo join images on productsrepo.id = images.imageId;").ToList();
    //     return users.ToList();
    // }

    // Product model data getting called and returned
    public static List<ProductModel> ProductsList;
    public static void GetProductsData(string brand, string category, int priceFrom, int priceTo, string connectionString)
    {
        MySqlConnection con = new MySqlConnection(connectionString);
        string filterQuery = DataLogic.GetProductDataLogic(brand, category, priceFrom, priceTo);
        var products = con.Query<ProductModel>(filterQuery);
        ProductsList = products.ToList();
    }

    // Image model data getting called and returned
    public static void GetImagesData(string connectionString)
    {
        foreach (ProductModel prod in ProductsList)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            string filterQuery = DataLogic.GetImagesDataLogic(prod.Id);
            var images = con.Query<string>(filterQuery);
            prod.images = images.ToList();
        }

    }


    // The main function calling the merged data from MergeData.cs file
    public static List<ProductModel> MergedResultantData(string brand, string category, int priceFrom, int priceTo, string connectionString)
    {
        GetProductsData(brand, category, priceFrom, priceTo, connectionString);
        GetImagesData(connectionString);
        return ProductsList;
    }


}
