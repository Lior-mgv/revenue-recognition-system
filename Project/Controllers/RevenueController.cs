using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using FinancesProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace FinancesProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly IContractService _contractService;
    private const string ClientUrl = "https://api.nbp.pl";
    private const string BaseApiUrl = "api/exchangerates/rates/a/";

    public RevenueController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetRevenue([FromQuery] int? productId, [FromQuery] string? currencyCode)
    {
        var revenue = productId == null
            ? await _contractService.GetTotalRevenue(false)
            : await _contractService.GetProductRevenue(productId.Value, false);
        return currencyCode != null ? await ConvertTo(revenue, currencyCode) : Ok(revenue);
    }
    
    [HttpGet("predicted")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetPredictedRevenue([FromQuery] int? productId, [FromQuery] string? currencyCode)
    {
        var revenue = productId == null
            ? await _contractService.GetTotalRevenue(true)
            : await _contractService.GetProductRevenue(productId.Value, true);
        return currencyCode != null ? await ConvertTo(revenue, currencyCode) : Ok(revenue);
    }

    private async Task<IActionResult> ConvertTo(decimal revenue, string currencyCode)
    {
        var client = new RestClient(ClientUrl);
        var request = new RestRequest(BaseApiUrl + currencyCode);

        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
            return response.StatusCode == HttpStatusCode.NotFound
                ? BadRequest("Incorrect currency code.")
                : StatusCode(500, $"Error occurred while trying to convert to {currencyCode}");
        var raw = response.Content;
        var json = JsonSerializer.Deserialize<JsonNode>(raw!);
        var exchangeRate = decimal.Parse(json?["rates"]?[0]?["mid"]?.ToString()!);
        return Ok(revenue / exchangeRate);
    }

}