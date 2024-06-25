using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.services;

namespace RevenueRecognitionSystem.controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly ContractService _contractService;

    public ContractsController(ContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateContract([FromBody] ContractDto contractDto)
    {
        try
        {
            var createdContract = await _contractService.CreateContractAsync(contractDto.ClientId, contractDto.SoftwareId, contractDto.SupportYears);
            return Ok(createdContract);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{contractId}/payments")]
    [Authorize]
    public async Task<IActionResult> MakePayment(int contractId, [FromBody] PaymentDto paymentDto)
    {
        try
        {
            var payment = await _contractService.MakePaymentAsync(contractId, paymentDto.Amount);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}