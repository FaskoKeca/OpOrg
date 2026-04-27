using System.ComponentModel.DataAnnotations;

namespace OpOrg.Models;

public class Operation
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на операцията/процедурата е задължително.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата и час на операцията/процедурата са задължителни.")]
    public DateTime DateTime { get; set; }

    public List<Consultation> Consultations { get; set; } = new List<Consultation>();

    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

    [Required(ErrorMessage = "Цената на операцията/процедурата е задължителна.")]
    public decimal Price { get; set; }

    // Foreign Keys
    [Required(ErrorMessage = "Пациентът е задължителен.")]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Лекарят е задължителен.")]
    public int DoctorId { get; set; }
}
