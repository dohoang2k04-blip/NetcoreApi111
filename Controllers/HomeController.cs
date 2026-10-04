using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using csmvc.Models;

namespace csmvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult list(int a,int b)
    {
       
        ViewBag.total=a+b;
        List<int> aa=new List<int>(){4,7,5,3,7,87};
        ViewBag.list=aa;
        
        return View();
    }

    [Route("products/shop")] //custom url

    public IActionResult showShopPage()
    {

        return View("/Views/ogani/shop-grid.cshtml");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
