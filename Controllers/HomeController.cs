using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpOrg.Models;
using OpOrg.Data;

namespace OpOrg.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        return View();
    }

}
