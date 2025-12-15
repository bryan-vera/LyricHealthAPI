using LyricHealthAPI.Domain;
using LyricHealthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LyricHealthAPI.Controllers
{
    [ApiController]
    [Route("claims")]
    public class ClaimController:ControllerBase
    {
        private IClaimValidationService _claimValidationService;
        public ClaimController(IClaimValidationService claimValidationService)
        {
            _claimValidationService = claimValidationService;
        }

        [HttpPost("validate")]
        public IActionResult ValidateClaim(IEnumerable<ClaimRequest> claimRequest)
        {
            var summary = _claimValidationService.ValidateClaims(claimRequest);
            return Ok(summary);
        }
    }
}
