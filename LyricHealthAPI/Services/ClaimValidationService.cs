using LyricHealthAPI.Domain;

namespace LyricHealthAPI.Services
{
    public class ClaimValidationService : IClaimValidationService
    {
        private const int MaxDiagnosisCodeLength = 5;
        private const int MinDiagnosisCodeLength = 3;
        private const string ApprovedStatus = "Approved";

        public ClaimsSummary ValidateClaims(IEnumerable<ClaimRequest> claimRequests)
        {
            var summary = new ClaimsSummary
            {
                TotalClaims = claimRequests.Count(),
                ValidClaims = 0,
                InvalidClaims = 0,
                TotalApprovedAmount = 0
            };

            foreach (var claim in claimRequests)
            {
                var isValidDiagnosisCode = IsDiagnosisCodeValid(claim);
                var isValidAmount = IsAmountValid(claim);
                if (isValidDiagnosisCode && isValidAmount)
                {
                    summary.ValidClaims++;
                    if (claim.Status.Equals(ApprovedStatus, StringComparison.CurrentCultureIgnoreCase))
                        summary.TotalApprovedAmount += claim.Amount;
                }
                else
                {
                    summary.InvalidClaims++;
                }
            }
            return summary;
        }

        private bool IsDiagnosisCodeValid(ClaimRequest claim)
        {
            if (string.IsNullOrWhiteSpace(claim.DiagnosisCode))
            {
                return false;
            }

            if (claim.DiagnosisCode.Length < MinDiagnosisCodeLength || claim.DiagnosisCode.Length > MaxDiagnosisCodeLength)
            {
                return false;
            }

            var firstChar = claim.DiagnosisCode[0];

            if (!char.IsLetter(firstChar))
            {
                return false;
            }

            foreach(var c in claim.DiagnosisCode.Skip(1))
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }


        private bool IsAmountValid(ClaimRequest claim)
        {
            if (claim.Amount <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
