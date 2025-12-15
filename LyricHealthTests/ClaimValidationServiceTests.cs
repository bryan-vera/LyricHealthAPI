using LyricHealthAPI.Domain;
using LyricHealthAPI.Services;
using Xunit;

namespace LyricHealthAPI.Tests
{
    public class ClaimValidationServiceTests
    {
        private readonly ClaimValidationService _claimValidationService = new ClaimValidationService();

        [Fact]
        public void InvalidClaim_ShouldBeRejected() { 
            var claim = new ClaimRequest
            {
                Id = 0,
                Provider = "Provider A",
                Amount = 67m,
                DiagnosisCode = "12A34",
                Status = "Pending"
            };

            var result = _claimValidationService.ValidateClaims(new List<ClaimRequest> { claim });
            Assert.Equal(1, result.TotalClaims);
            Assert.Equal(0, result.ValidClaims);
            Assert.Equal(1, result.InvalidClaims);
            Assert.Equal(0m, result.TotalApprovedAmount);
        }

        [Fact]
        public void ValidClaim_ShouldBeApproved() {
            var claim = new ClaimRequest
            {
                Id = 1,
                Provider = "Clinic A",
                Amount = 100.5m,
                DiagnosisCode = "A01",
                Status = "Approved"
            };
            var result = _claimValidationService.ValidateClaims(new List<ClaimRequest> { claim });
            Assert.Equal(1, result.TotalClaims);
            Assert.Equal(1, result.ValidClaims);
            Assert.Equal(0, result.InvalidClaims);
            Assert.Equal(100.5m, result.TotalApprovedAmount);
        }

        [Fact]
        public void MixedClaims_ShouldBeProcessedCorrectly() {
            var claims = new List<ClaimRequest>
            {
                new ClaimRequest
                {
                    Id = 1,
                    Provider = "Clinic A",
                    Amount = 100.5m,
                    DiagnosisCode = "A01",
                    Status = "Approved"
                },
                new ClaimRequest
                {
                    Id = 2,
                    Provider = "Clinic B",
                    Amount = -50m,
                    DiagnosisCode = "123",
                    Status = "Pending"
                }
            };
            var result = _claimValidationService.ValidateClaims(claims);
            Assert.Equal(2, result.TotalClaims);
            Assert.Equal(1, result.ValidClaims);
            Assert.Equal(1, result.InvalidClaims);
            Assert.Equal(100.5m, result.TotalApprovedAmount);
        }

        [Fact]
        public void ValidClaim_NonApprovedStatus_ShouldNotIncreaseApprovedAmount() {
            var claim = new ClaimRequest
            {
                Id = 3,
                Provider = "Clinic C",
                Amount = 20000m,
                DiagnosisCode = "A02",
                Status = "Pending"
            };
            var result = _claimValidationService.ValidateClaims(new List<ClaimRequest> { claim });
            Assert.Equal(1, result.TotalClaims);
            Assert.Equal(1, result.ValidClaims);
            Assert.Equal(0, result.InvalidClaims);
            Assert.Equal(0m, result.TotalApprovedAmount);
        }

    }
}
