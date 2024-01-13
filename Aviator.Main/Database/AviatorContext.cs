using Microsoft.EntityFrameworkCore;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Database;

public class AviatorContext(DbContextOptions<AviatorContext> options) : DbContext(options)
{
    public required DbSet<Endpoint> Outputs { get; init; }
}