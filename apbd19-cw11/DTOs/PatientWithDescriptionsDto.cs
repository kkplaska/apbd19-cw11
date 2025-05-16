using apbd19_cw11.Models;

namespace apbd19_cw11.DTOs;

public class PatientWithDescriptionsDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public List<PresWithMedicsAndDocsDto> Prescriptions { get; set; }
}

public class PresWithMedicsAndDocsDto
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}