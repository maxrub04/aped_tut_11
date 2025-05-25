using tut_11.DTOs;
using tut_11.Services;

namespace tut_11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Prescriptions_Controller : ControllerBase {
    private readonly IPrescriptionService _service;

    public Prescriptions_Controller(IPrescriptionService service) {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(AddPrescriptionRequest request) {
        try {
            await _service.AddPrescriptionAsync(request);
            return Ok("Prescription added.");
        } catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }
}
