using MVCAPI.Repository;
using MVCAPI.Models;

namespace MVCAPI.Repository;

public class MergeData
{
    // getting the products and image data and merging them together here
    public static List<ProductModel> ResultantData(string brand, string category, int priceFrom, int priceTo, string connectionString)
    {
        var productData = ProductRepository.GetProductsData(brand, category, priceFrom, priceTo, connectionString);
        var imagesData = ProductRepository.GetImagesData(brand, category, priceFrom, priceTo, connectionString);

        for (int i = 0; i < productData.Count(); i++)
        {
            productData[i].images = ConvertObjecttoList(imagesData[i]);
        }
        return productData.ToList();
    }

    // converting the imageModel class into a list
    public static List<string> ConvertObjecttoList(ImageModel imageModel)
    {
        List<string> imagesList = new List<string>();
        if (imageModel.Image1 != "")
        {
            imagesList.Add(imageModel.Image1);
        }

        if (imageModel.Image2 != "")
        {
            imagesList.Add(imageModel.Image2);
        }

        if (imageModel.Image3 != "")
        {
            imagesList.Add(imageModel.Image3);
        }

        if (imageModel.Image4 != "")
        {
            imagesList.Add(imageModel.Image4);
        }

        if (imageModel.Image5 != "")
        {
            imagesList.Add(imageModel.Image5);
        }

        if (imageModel.Image6 != "")
        {
            imagesList.Add(imageModel.Image6);
        }

        return imagesList;
    }

}