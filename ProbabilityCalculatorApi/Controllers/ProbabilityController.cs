using Microsoft.AspNetCore.Mvc;
using ProbabilityCalculatorApi.Model;
using ProbabilityCalculatorApi.Service;

namespace ProbabilityCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbabilityController : ControllerBase
    {
        private readonly IProbabilityCalculatorService _probabilityCalculatorService;

        public ProbabilityController(IProbabilityCalculatorService probabilityCalculatorService)
        {
            _probabilityCalculatorService = probabilityCalculatorService ?? throw new ArgumentNullException(nameof(probabilityCalculatorService));
        }
        [HttpPost("calculation")]
        public IActionResult Calculation([FromBody]ProbabilityCalculationModel probabilityCalculationModel)
        {
            if(probabilityCalculationModel == null)
            {
                return BadRequest();
            }

            var result = _probabilityCalculatorService.CalculateProbability(probabilityCalculationModel);

            return Ok(result);
        }

        [HttpGet("calculations")]
        public IActionResult Calculations()
        {
            return Ok(_probabilityCalculatorService.ListCalculations());
        }
    }
}
