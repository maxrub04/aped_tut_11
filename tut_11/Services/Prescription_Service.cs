using System;
using System.Linq;
using System.Threading.Tasks;
using tut_11.DTOs;
using tut_11.Models;




namespace tut_11.Services;

public class Prescription_Service : IPrescriptionService {
    private readonly MedicalDbContext _context;

    public Prescription_Service(MedicalDbContext context) {
        _context = context;
    }

    public async Task AddPrescriptionAsync(AddPrescriptionRequest request) {
        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate must be after or equal to Date");

        if (request.Medicaments.Count > 10)
            throw new ArgumentException("A prescription cannot include more than 10 medications");

        foreach (var m in request.Medicaments) {
            if (!await _context.Medicaments.AnyAsync(med => med.IdMedicament == m.IdMedicament))
                throw new ArgumentException($"Medicament with ID {m.IdMedicament} does not exist");
        }

        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null) {
            patient = new Patient {
                IdPatient = request.Patient.IdPatient,
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
        }

        var prescription = new Prescription {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = request.IdDoctor,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
}
