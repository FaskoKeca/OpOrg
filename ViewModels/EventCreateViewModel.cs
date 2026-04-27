using System.ComponentModel.DataAnnotations;

namespace OpOrg.ViewModels;

public class EventCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Заглавието на събитието е задължително.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата и час на събитието са задължителни.")]
    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; } = DateTime.Now;

    public string Description { get; set; } = string.Empty;

    //Foreign Keys
    public int PatientId { get; set; }
    public string? PatientName { get; set; }
}