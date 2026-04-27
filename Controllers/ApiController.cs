using Microsoft.AspNetCore.Mvc;
using OpOrg.Data;
using Microsoft.EntityFrameworkCore;

namespace OpOrg.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly AppDbContext _db;

    public ApiController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("doctors")]
    public async Task<ActionResult<List<object>>> GetDoctors()
    {
        var doctors = await _db.Doctors
            .Select(d => new { d.Id, d.Name })
            .ToListAsync();
        return Ok(doctors);
    }

    [HttpGet("doctors/{id}")]
    public async Task<ActionResult<object>> GetDoctor(int id)
    {
        var doctor = await _db.Doctors
            .Where(d => d.Id == id)
            .Select(d => new { d.Id, d.Name })
            .FirstOrDefaultAsync();
        
        if (doctor == null)
            return NotFound();
        
        return Ok(doctor);
    }

    [HttpGet("patients")]
    public async Task<ActionResult<List<object>>> GetPatients()
    {
        var patients = await _db.Patients
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();
        return Ok(patients);
    }

    [HttpGet("patients/{id}")]
    public async Task<ActionResult<object>> GetPatient(int id)
    {
        var patient = await _db.Patients
            .Where(p => p.Id == id)
            .Select(p => new { p.Id, p.Name })
            .FirstOrDefaultAsync();
        
        if (patient == null)
            return NotFound();
        
        return Ok(patient);
    }
}
