using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCAPI.Models;
using MVCAPI.Repository;
using Newtonsoft.Json;
using PagedList;
using MVCAPI.BusinessLogic;

namespace MVCAPI.Controllers;

public class ApiController : Controller
{
    private readonly ILogger<ApiController> _logger;
    private readonly IConfiguration _Configuration;
    public ApiController(ILogger<ApiController> logger, IConfiguration _configuration)
    {
        _logger = logger;
        this._Configuration = _configuration;
    }

    // the main api that is calling the resultant data!
    public string Result(string? brand = "", string category = "", int priceFrom = 0, int priceTo = 0, int page = 1)
    {
        int pageSize = 6;
        int pageIndex = 1;
        pageIndex = Convert.ToInt32(page);
        string connectionString = _Configuration.GetValue<string>("ConnectionStrings:default");
        var data = ProductRepository.MergedResultantData(brand, category, priceFrom, priceTo, connectionString);
        IPagedList<ProductModel> list = data.ToPagedList(pageIndex, pageSize);
        var json = JsonConvert.SerializeObject(list);
        return json;

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}