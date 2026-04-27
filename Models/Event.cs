using System.ComponentModel.DataAnnotations;

namespace OpOrg.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Заглавието на събитието е задължително.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата и час на събитието са задължителни.")]
    public DateTime DateTime { get; set; }

    public string Description { get; set; } = string.Empty;

    //Foreign Keys
    public int PatientId { get; set; }
}
