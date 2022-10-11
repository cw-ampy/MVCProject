namespace MVCAPI.BusinessLogic;

// Logic for filtering the data by making the sql query
public class DataLogic
{
    // get the products data
    public static string GetProductDataLogic(string brand, string category, int priceFrom, int priceTo)
    {
        string filteredQuery = "";
        string query = "Select * from products_aman WHERE ";
        filteredQuery = filterTheQuery(query, brand, category, priceFrom, priceTo);
        if (filteredQuery == "")
        {
            filteredQuery = "select *from products_aman";
        }
        return filteredQuery;
    }

    // get the image data
    public static string GetImagesDataLogic(int id)
    {
        string filteredQuery = "";
        string query = "select image_link from image_aman ";
        filteredQuery = filterImageQuery(query, id);
        return filteredQuery;
    }

    // function to make the sql query based on filters
    public static string filterTheQuery(string query, string brand, string category, int priceFrom, int priceTo)
    {
        if (brand != "") // checking if the brand exists in the products table
        {
            query = query + $"brand IN ({brand})";
            if (category != "") // checking if the category exists in the products table
            {
                query = query + $" AND category IN ({category})";
                if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table
                {
                    query = query + $" AND price between {priceFrom} AND {priceTo}";
                }
            }
        }
        else if (category != "") // checking if the category exists in the products table if not brand given
        {
            query = query + $"category IN ({category})";
            if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table
            {
                query = query + $" AND price between {priceFrom} AND {priceTo}";
            }
        }
        else if (priceFrom != 0 && priceTo != 0) // checking if the price between exists in the products table if not brand and category given 
        {
            query = query + $" AND price between {priceFrom} AND {priceTo}";
        }
        else // if no parameters are available
        {
            query = "";
        }
        return query;
    }
    public static string filterImageQuery(string query, int id)
    {
        query = query + $" WHERE id={id}";
        return query;
    }
}