using FinancesProject.DTO;
using FinancesProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancesProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> CreateContract([FromBody] ContractForm form)
    {
        await _contractService.CreateContractAsync(form);
        return Created();
    }

    [HttpPost("pay/{contractId:int}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> PayForContract([FromQuery] decimal sum, int contractId)
    {
        await _contractService.PayForContractAsync(sum, contractId);
        return Ok();
    }

}