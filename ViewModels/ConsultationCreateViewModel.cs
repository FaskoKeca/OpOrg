using System.ComponentModel.DataAnnotations;

namespace OpOrg.ViewModels;

public class ConsultationCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на консултиращия лекар е задължително.")]
    [StringLength(50)]
    public string ConsultantName { get; set; } = string.Empty;


    [Required(ErrorMessage = "УИН-ът на консултиращия лекар е задължителен.")]
    public string UIN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата и час на консултацията са задължителни.")]
    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Отделът за консултация е задължителен.")]
    public string ConsultingDepartment { get; set; } = string.Empty;

    [Required(ErrorMessage = "Заключението е задължително.")]
    public string Conclusion { get; set; } = string.Empty;

    public int OperationId { get; set; }
    public string? OperationName { get; set; }
}