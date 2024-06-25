using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.services;

namespace RevenueRecognitionSystem.controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("individual")]
    [Authorize]
    public async Task<IActionResult> AddIndividualClient([FromBody] IndividualClientDto clientDto)
    {
        await _clientService.AddClientAsync(clientDto);
        return Ok();
    }

    [HttpPost("company")]
    [Authorize]
    public async Task<IActionResult> AddCompanyClient([FromBody] CompanyClientDto clientDto)
    {
        await _clientService.AddClientAsync(clientDto);
        return Ok();
    }

    [HttpPut("individual")]
    [Authorize]
    public async Task<IActionResult> UpdateIndividualClient([FromBody] IndividualClientDto clientDto)
    {
        try
        {
            await _clientService.UpdateClientAsync(clientDto);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("company")]
    [Authorize]
    public async Task<IActionResult> UpdateCompanyClient([FromBody] CompanyClientDto clientDto)
    {
        try
        {
            await _clientService.UpdateClientAsync(clientDto);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveClient(int id)
    {
        try
        {
            await _clientService.RemoveClientAsync(id);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("individual")]
    [Authorize]
    public async Task<IActionResult> GetAllIndividualClients()
    {
        var clients = await _clientService.GetAllIndividualClientsAsync();
        return Ok(clients);
    }

    [HttpGet("company")]
    [Authorize]
    public async Task<IActionResult> GetAllCompanyClients()
    {
        var clients = await _clientService.GetAllCompanyClientsAsync();
        return Ok(clients);
    }
}