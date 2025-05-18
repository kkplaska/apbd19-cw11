using System.ComponentModel.DataAnnotations;
using apbd19_cw11.Models;

namespace apbd19_cw11.DTOs;

public class PrescriptionDto
{
    public PatientDto Patient { get; set; }
    public ICollection<AlterMedicamentDto> Medicaments { get; set; } = new List<AlterMedicamentDto>();
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}

public class PatientDto
{
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}

public class AlterMedicamentDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
}