# Healthcare Claims Processor API
### Overview
This project implements a small API using ASP.NET CORE 8 that validates healthcare claims 
and returns a summary report of the valid/invalid claims.

### Business Logic
A claim is considered valid if:
- The amount is greater than 0
- The diagnosis code: 
	- Is not empty
	- Starts with a letter followed by 2-4 digits (e.g., A01, B2321)
	- Total length is between 3 and 5 characters
- Only valid claims with status "Approved" are included in the total approved amount

### Requirements
- .NET 8 SDK or higher
- xUnit for tests
### How to Run
```bash
dotnet build
dotnet run --project LyricHealthAPI
```
API will be available at http://localhost:5009/claims/validate

### How to Test

```bash
dotnet test
```
Example Request

```bash
curl -X POST http://localhost:5009/claims/validate \
-H "Content-Type: application/json" \
-d '[{"Id":1,"Provider":"Clinic
A","Amount":100.5,"DiagnosisCode":"A01","Status":"Approved"}]'
```

Example Response
```bash
{
"totalClaims": 1,
"validClaims": 1,
"invalidClaims": 0,
"totalApprovedAmount": 100.5
}
```