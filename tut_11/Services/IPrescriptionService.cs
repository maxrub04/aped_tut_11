using tut_11.DTOs;

namespace tut_11.Services;

using System.Threading.Tasks;


public interface IPrescriptionService
{
    Task AddPrescriptionAsync(AddPrescriptionRequest request);
}
