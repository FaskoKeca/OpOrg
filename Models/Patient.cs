using System.ComponentModel.DataAnnotations;

namespace OpOrg.Models;

public class Patient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името е задължително.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Датата на раждане е задължителна.")]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "EGN е задължително.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "EGN трябва да бъде с дължина 10 символа.")]
    public string EGN { get; set; } = string.Empty;
    
    public List<Operation> Operations { get; set; } = new List<Operation>();
    
}
