using System.ComponentModel.DataAnnotations;

namespace OpOrg.ViewModels;

public class PatientCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името е задължително.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<OperationCreateViewModel> Operations { get; set; } = new List<OperationCreateViewModel>();

    public List<EventCreateViewModel> Events { get; set; } = new List<EventCreateViewModel>();
}