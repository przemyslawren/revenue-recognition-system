using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.services;

namespace RevenueRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly ContractService _contractService;

        public RevenueController(ContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentRevenue([FromQuery] string currency = "PLN")
        {
            try
            {
                var revenue = await _contractService.CalculateRevenue(currency);
                return Ok(revenue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("predicted")]
        [Authorize]
        public async Task<IActionResult> GetPredictedRevenue([FromQuery] string currency = "PLN")
        {
            try
            {
                var revenue = await _contractService.CalculatePredictedRevenue(currency);
                return Ok(revenue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}