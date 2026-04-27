using OpOrg.Models;
using OpOrg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace OpOrg.Controllers;

public class OperationController : Controller
{
    private readonly AppDbContext _db;

    public OperationController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var operations = await _db.Operations.ToListAsync();
        return View(operations);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Operation operation)
    {
        if (ModelState.IsValid)
        {
            _db.Operations.Add(operation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(operation);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operation = await _db.Operations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (operation == null)
        {
            return NotFound();
        }

        return View(operation);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operation = await _db.Operations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (operation == null)
        {
            return NotFound();
        }

        return View(operation);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var operation = await _db.Operations.FindAsync(id);
        if (operation != null)
        {
            _db.Operations.Remove(operation);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
}