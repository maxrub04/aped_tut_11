using System;
using System.Collections.Generic;

namespace tut_11.DTOs;

public class AddPrescriptionRequest {
    public PatientDto Patient { get; set; }
    public List<MedicamentPrescriptionDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}