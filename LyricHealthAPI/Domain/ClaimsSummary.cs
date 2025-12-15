namespace LyricHealthAPI.Domain
{
    public class ClaimsSummary
    {
        public int TotalClaims { get; set; }
        public int ValidClaims { get; set; }
        public int InvalidClaims { get; set; }
        public decimal TotalApprovedAmount { get; set; }
    }
}
