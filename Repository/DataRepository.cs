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
    static MySqlConnection con = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=db_aman");
    public static List<dynamic> GetData()
    {
        var users = con.Query<dynamic>("select *from productsrepo join images on productsrepo.id = images.imageId;").ToList();
        return users.ToList();
    }


    public static List<dynamic> GetData(string brand, string category, int priceFrom, int priceTo)
    {
        MySqlConnection con = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=db_aman");
        string filterQuery = "Select * from productsrepo join images on productsrepo.id = images.imageId WHERE ";
        if (brand != "")
        {
            filterQuery = filterQuery + $"brand IN ({brand})";
            if (category != "")
            {
                filterQuery = filterQuery + $" AND category IN ({category})";
                if (priceFrom != 0 && priceTo != 0)
                {
                    filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}";
                }
            }
        }
        else if (category != "")
        {
            filterQuery = filterQuery + $"category IN ({category})";
            if (priceFrom != 0 && priceTo != 0)
            {
                filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}";
            }
        }
        else if (priceFrom != 0 && priceTo != 0)
        {
            filterQuery = filterQuery + $" AND price between {priceFrom} AND {priceTo}";
        }
        else
        {
            filterQuery = "select *from productsrepo join images on productsrepo.id = images.imageId;";
        }
        var users = con.Query<dynamic>(filterQuery);
        return users.ToList();


    }

}


    // var jsonUsers = JsonConvert.SerializeObject(users);
    //     dynamic jsonObjUsers = JsonConvert.DeserializeObject(jsonUsers);

    //     var jsonImages = JsonConvert.SerializeObject(images);
    //     dynamic jsonObjImages = JsonConvert.DeserializeObject(jsonImages);


    //     for(int i = 0; i <= images.Count(); i++){
    //       System.Console.WriteLine(output[i]);
    //     }
    //     System.Console.WriteLine(images);