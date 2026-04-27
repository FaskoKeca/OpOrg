using System.ComponentModel.DataAnnotations;

namespace OpOrg.ViewModels;

public class DoctorCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името е задължително.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "УИН е задължителен.")]
    public string UIN { get; set; } = string.Empty;

    public List<OperationCreateViewModel> Operations { get; set; } = new List<OperationCreateViewModel>();
}