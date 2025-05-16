using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Models;

[PrimaryKey(nameof(IdPrescription))]
[Table("Prescription")]
public class Prescription
{
    public int IdPrescription { get; set; }
    
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; }
}
