using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Models;

[PrimaryKey(nameof(IdPrescription))]
[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }

    [ForeignKey(nameof(IdPatient))]
    public Patient Patient { get; set; }
    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; }
}
