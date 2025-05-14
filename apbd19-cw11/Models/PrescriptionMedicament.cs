using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Models;

[PrimaryKey(nameof(IdPrescription))]
[Table("Prescription_Medicament")]
public class PrescriptionMedicament
{

    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; }
    
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }
    
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
    
}