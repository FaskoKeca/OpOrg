using System.ComponentModel.DataAnnotations;

namespace OpOrg.Models;

public class Operation
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на операцията/процедурата е задължително.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Описание на операцията/процедурата е задължително.")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата и час на операцията/процедурата са задължителни.")]
    public DateTime DateTime { get; set; }

    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

    // Foreign Keys
    public int PatientId { get; set; }

    public int DoctorId { get; set; }
}
