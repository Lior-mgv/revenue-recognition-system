using FinancesProject.DTO;
using FinancesProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancesProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("individual/add")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddIndividualClient([FromBody] IndividualClientForm clientForm)
    {
        await _clientService.AddIndividualClientAsync(clientForm);
        return Created();
    }

    [HttpPost("company/add")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddCompany([FromBody] CompanyForm clientForm)
    {
        await _clientService.AddCompanyClientAsync(clientForm);
        return Created();
    }

    [HttpDelete("/individual/delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteIndividualClient(int id)
    {
        await _clientService.DeleteIndividualClientAsync(id);
        return NoContent();
    }

    [HttpPatch("/individual/update/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] IndividualClientPatch patch)
    {
        await _clientService.UpdateIndividualClientAsync(id, patch);
        return NoContent();
    }
    [HttpPatch("/company/update/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyPatch patch)
    {
        await _clientService.UpdateCompanyClientAsync(id, patch);
        return NoContent();
    }


}