using System.ComponentModel.DataAnnotations;

namespace apbd19_cw11.Models;

public class Patient
{
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}