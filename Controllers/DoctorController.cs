using OpOrg.Models;
using OpOrg.Data;
using OpOrg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpOrg.Controllers;

public class DoctorController : Controller
{
    private readonly AppDbContext _db;

    public DoctorController(AppDbContext db)
    {
        _db = db;
    }

    public bool DoctorExists(int id)
    {
        return _db.Doctors.Any(d => d.Id == id);
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _db.Doctors.ToListAsync();
        return View(doctors);
    }

    public IActionResult Create()
    {
        return View(new DoctorCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var doctor = new Doctor
            {
                Name = viewModel.Name,
                UIN = viewModel.UIN
            };

            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(viewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _db.Doctors
            .Include(d => d.Operations)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _db.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        var viewModel = new DoctorCreateViewModel
        {
            Id = doctor.Id,
            Name = doctor.Name,
            UIN = doctor.UIN
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DoctorCreateViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var doctor = await _db.Doctors.FindAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.Name = viewModel.Name;
                doctor.UIN = viewModel.UIN;

                _db.Update(doctor);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(viewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _db.Doctors
            .FirstOrDefaultAsync(d => d.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await _db.Doctors.FindAsync(id);
        if (doctor != null)
        {
            _db.Doctors.Remove(doctor);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}