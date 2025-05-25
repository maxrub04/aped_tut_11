using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tut_11.DTOs;

namespace tut_11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Patients_Controller : ControllerBase {
    private readonly MedicalDbContext _context;

    public Patients_Controller(MedicalDbContext context) {
        _context = context;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDetailsDto>> GetPatient(int id) {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
            return NotFound();

        return new PatientDetailsDto {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(pr => new PrescriptionDto {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorDto {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName
                    },
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDto {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList()
                }).ToList()
        };
    }
}
