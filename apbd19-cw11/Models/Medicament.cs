using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Models;

[PrimaryKey(nameof(IdMedicament))]
public class Medicament
{
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
}