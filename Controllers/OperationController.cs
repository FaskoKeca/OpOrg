using OpOrg.Models;
using OpOrg.Data;
using OpOrg.ViewModels;
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

    public bool OperationExists(int id)
    {
        return _db.Operations.Any(e => e.Id == id);
    }

    public async Task<IActionResult> Index()
    {
        var operations = await _db.Operations.ToListAsync();
        return View(operations);
    }

    public IActionResult Create()
    {
        return View(new OperationCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OperationCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var operation = new Operation
            {
                Name = viewModel.Name,
                Notes = viewModel.Notes,
                DateTime = viewModel.DateTime,
                Status = viewModel.Status,
                Price = viewModel.Price,
                PatientId = viewModel.PatientId,
                DoctorId = viewModel.DoctorId
            };

            _db.Operations.Add(operation);
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

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operation = await _db.Operations.FindAsync(id);
        if (operation == null)
        {
            return NotFound();
        }

        var viewModel = new OperationCreateViewModel
        {
            Id = operation.Id,
            Name = operation.Name,
            Notes = operation.Notes,
            DateTime = operation.DateTime,
            Status = operation.Status,
            Price = operation.Price,
            PatientId = operation.PatientId,
            DoctorId = operation.DoctorId
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OperationCreateViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var operation = await _db.Operations.FindAsync(id);
                if (operation == null)
                {
                    return NotFound();
                }

                operation.Name = viewModel.Name;
                operation.Notes = viewModel.Notes;
                operation.DateTime = viewModel.DateTime;
                operation.Status = viewModel.Status;
                operation.Price = viewModel.Price;
                operation.PatientId = viewModel.PatientId;
                operation.DoctorId = viewModel.DoctorId;

                _db.Update(operation);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationExists(viewModel.Id))
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

    public async Task<IActionResult> Search(string searchString)
    {
        var operations = from o in _db.Operations
                         select o;

        if (!string.IsNullOrEmpty(searchString))
        {
            operations = operations.Where(o => o.Name.Contains(searchString));
        }

        return View(await operations.ToListAsync());
    }

    public async Task<IActionResult> Consultations(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operation = await _db.Operations
            .Include(o => o.Consultations)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (operation == null)
        {
            return NotFound();
        }

        ViewBag.OperationId = id;
        return View(operation.Consultations);
    }

    public async Task<IActionResult> CreateConsultation(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operation = await _db.Operations.FindAsync(id);
        if (operation == null)
        {
            return NotFound();
        }

        var viewModel = new ConsultationCreateViewModel { OperationId = id.Value };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConsultation(int id, ConsultationCreateViewModel viewModel)
    {
        if (id != viewModel.OperationId)
        {
            return NotFound();
        }

        var operation = await _db.Operations.FindAsync(id);
        if (operation == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var consultation = new Consultation
            {
                ConsultantName = viewModel.ConsultantName,
                UIN = viewModel.UIN,
                DateTime = viewModel.DateTime,
                ConsultingDepartment = viewModel.ConsultingDepartment,
                Conclusion = viewModel.Conclusion,
                OperationId = viewModel.OperationId
            };

            _db.Consultations.Add(consultation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Consultations), new { id });
        }
        return View(viewModel);
    }

    public async Task<IActionResult> DeleteConsultation(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consultation = await _db.Consultations.FirstOrDefaultAsync(c => c.Id == id);
        if (consultation == null)
        {
            return NotFound();
        }

        return View(consultation);
    }

    [HttpPost, ActionName("DeleteConsultation")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConsultationConfirmed(int id)
    {
        var consultation = await _db.Consultations.FindAsync(id);
        if (consultation != null)
        {
            int operationId = consultation.OperationId;
            _db.Consultations.Remove(consultation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Consultations), new { id = operationId });
        }
        return RedirectToAction(nameof(Index));
    }
}