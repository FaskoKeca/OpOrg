using System.ComponentModel.DataAnnotations;

namespace OpOrg.ViewModels;

public class OperationCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на операцията/процедурата е задължително.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата на операцията/процедурата е задължителна.")]
    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; } = DateTime.Now;

    public List<ConsultationCreateViewModel> Consultations { get; set; } = new List<ConsultationCreateViewModel>();

    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

    [Required(ErrorMessage = "Цената на операцията/процедурата е задължителна.")]
    public decimal Price { get; set; }

    // Foreign Keys
    [Required(ErrorMessage = "Пациентът е задължителен.")]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Лекарят е задължителен.")]
    public int DoctorId { get; set; }

    // Navigation properties for display
    public string? PatientName { get; set; }
    public string? DoctorName { get; set; }
}