using LyricHealthAPI.Domain;

namespace LyricHealthAPI.Services
{
    public interface IClaimValidationService
    {
        ClaimsSummary ValidateClaims(IEnumerable<ClaimRequest> claimRequests);
    }
}