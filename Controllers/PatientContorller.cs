using OpOrg.Models;
using OpOrg.Data;
using OpOrg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace OpOrg.Controllers;

public class PatientController : Controller
{
    private readonly AppDbContext _db;

    public PatientController(AppDbContext db)
    {
        _db = db;
    }

    public bool PatientExists(int id)
    {
        return _db.Patients.Any(e => e.Id == id);
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _db.Patients.ToListAsync();
        return View(patients);
    }

    public IActionResult Create()
    {
        return View(new PatientCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PatientCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var patient = new Patient
            {
                Name = viewModel.Name
            };

            _db.Patients.Add(patient);
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

        var patient = await _db.Patients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _db.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        var viewModel = new PatientCreateViewModel
        {
            Id = patient.Id,
            Name = patient.Name
        };

        return View(viewModel);
    }   

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PatientCreateViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var patient = await _db.Patients.FindAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }

                patient.Name = viewModel.Name;

                _db.Update(patient);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(viewModel.Id))
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

         var patient = await _db.Patients
             .FirstOrDefaultAsync(m => m.Id == id);
         if (patient == null)
         {
             return NotFound();
         }

         return View(patient);
     }

     [HttpPost, ActionName("Delete")]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> DeleteConfirmed(int id)
     {
         var patient = await _db.Patients.FindAsync(id);
         if (patient != null)
         {
             _db.Patients.Remove(patient);
             await _db.SaveChangesAsync();
         }
         return RedirectToAction(nameof(Index));
     }

     public async Task<IActionResult> Operations(int? id)
     {
         if (id == null)
         {
             return NotFound();
         }

         var patient = await _db.Patients
             .Include(p => p.Operations)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (patient == null)
         {
             return NotFound();
         }

         ViewBag.PatientId = id;
         return View(patient.Operations);
     }
     
     public async Task<IActionResult> Events(int? id)
     {
         if (id == null)
         {
             return NotFound();
         }

         var patient = await _db.Patients
             .Include(p => p.Events)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (patient == null)
         {
             return NotFound();
         }

         ViewBag.PatientId = id;
         return View(patient.Events);
     }

     public async Task<IActionResult> CreateEvent(int? id)
     {
         if (id == null)
         {
             return NotFound();
         }

         var patient = await _db.Patients.FindAsync(id);
         if (patient == null)
         {
             return NotFound();
         }

         var viewModel = new EventCreateViewModel { PatientId = id.Value };
         return View(viewModel);
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEvent(int id, EventCreateViewModel viewModel)
    {
        if (id != viewModel.PatientId)
        {
            return NotFound();
        }

        var patient = await _db.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var eventObj = new Event
            {
                Title = viewModel.Title,
                DateTime = viewModel.DateTime,
                Description = viewModel.Description,
                PatientId = viewModel.PatientId
            };

            _db.Events.Add(eventObj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Events), new { id });
        }
        return View(viewModel);
    }

    public async Task<IActionResult> DeleteEvent(int? id)
     {
         if (id == null)
         {
             return NotFound();
         }

         var eventObj = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
         if (eventObj == null)
         {
             return NotFound();
         }

         return View(eventObj);
     }

     [HttpPost, ActionName("DeleteEvent")]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> DeleteEventConfirmed(int id)
     {
         var eventObj = await _db.Events.FindAsync(id);
         if (eventObj != null)
         {
             int patientId = eventObj.PatientId;
             _db.Events.Remove(eventObj);
             await _db.SaveChangesAsync();
             return RedirectToAction(nameof(Events), new { id = patientId });
         }
         return RedirectToAction(nameof(Index));
     }
}