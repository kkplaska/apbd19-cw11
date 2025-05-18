using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Models;

[PrimaryKey(nameof(IdPatient))]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}