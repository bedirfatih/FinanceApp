using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Transaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.UtcNow;
}
