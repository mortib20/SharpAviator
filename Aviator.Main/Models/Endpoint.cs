using System.ComponentModel.DataAnnotations;

namespace Aviator.Main.Models;

public class Endpoint
{
    [Key] public Guid Guid { get; init; }

    public required Decoder Decoder { get; set; }
    public required Protocol Protocol { get; set; }

    [MaxLength(30)] public required string Address { get; set; }

    [Range(ushort.MinValue, ushort.MaxValue)]
    public required int Port { get; set; }
}