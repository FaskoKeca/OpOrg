using OpOrg.Models;
using OpOrg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace OpOrg.Controllers;
public class ConsultationController : Controller
{
    private readonly AppDbContext _db;

    public ConsultationController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var consultations = await _db.Consultations.ToListAsync();
        return View(consultations);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consultation = await _db.Consultations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (consultation == null)
        {
            return NotFound();
        }

        return View(consultation);
    }
}