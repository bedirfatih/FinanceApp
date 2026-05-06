using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("mock")]
public class MockController : ControllerBase
{
    [HttpGet("exchange-rates")]
    public IActionResult GetExchangeRates() => Ok(new
    {
        baseCurrency = "TRY",
        rates = new
        {
            EUR = 0.026m,
            USD = 0.028m,
            TRY = 1.0m
        },
        updatedAt = DateTime.UtcNow
    });

    [HttpPost("bank-transfer")]
    public IActionResult BankTransfer([FromBody] BankTransferRequest request)
    {
        var success = Random.Shared.Next(2) == 1;
        return Ok(new
        {
            fromAccount = request.FromAccount,
            toAccount = request.ToAccount,
            amount = request.Amount,
            status = success ? "Success" : "Failed",
            message = success
                ? "Transfer completed successfully."
                : "Transfer failed. Please try again later."
        });
    }
}

public record BankTransferRequest(string FromAccount, string ToAccount, decimal Amount);
