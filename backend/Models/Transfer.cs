using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Transfer
{
    public int Id { get; set; }

    public int FromUserId { get; set; }

    [ForeignKey(nameof(FromUserId))]
    public User? FromUser { get; set; }

    public int ToUserId { get; set; }

    [ForeignKey(nameof(ToUserId))]
    public User? ToUser { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
