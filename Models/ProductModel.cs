namespace MVCAPI.Models;

// The product model for products table
public class ProductModel
{
    public int Id { get; set; }
    public string Title {get; set;} = "";
    public string Description {get; set;}
    public float Price {get; set;}
    public float Discount {get; set;}
    public float Rating {get; set;}
    public int Stock {get; set;}
    public string Brand {get; set;}
    public string Category {get; set;}
    public string Thumbnail {get; set;}
    public List<string> images {get; set;}

}
