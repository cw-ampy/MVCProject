using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCAPI.Models;
using MVCAPI.Repository;
using Newtonsoft.Json;
using PagedList;

namespace MVCAPI.Controllers;

public class ApiController : Controller
{
    private readonly ILogger<ApiController> _logger;

    public ApiController(ILogger<ApiController> logger)
    {
        _logger = logger;
    }

    // controller for all the api data
    public List<dynamic> GetData()
    {
        return ProductRepository.GetData();
    }

    // controller for data based on filters 
    public IPagedList<dynamic> ProductsData(string? brand = "", string category = "", int priceFrom = 0, int priceTo = 0, int page = 1)
    {
        int pageSize = 6;
        int pageIndex = 1;
        pageIndex = Convert.ToInt32(page);
        var data = ProductRepository.GetData(brand, category, priceFrom, priceTo);
        IPagedList<dynamic> list = data.ToPagedList(pageIndex, pageSize);
        return list;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
