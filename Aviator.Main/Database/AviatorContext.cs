using Aviator.Main.Models;
using Microsoft.EntityFrameworkCore;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Database;

public class AviatorContext(DbContextOptions<AviatorContext> options) : DbContext(options)
{
    public required DbSet<Endpoint> Outputs { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Endpoint>()
            .HasData(new List<Endpoint>
            {
                new()
                {
                    Guid = Guid.NewGuid(),
                    Decoder = Decoder.DumpVdl2,
                    Protocol = Protocol.Udp,
                    Address = "feed.airframes.io",
                    Port = 5552
                },
                new()
                {
                    Guid = Guid.NewGuid(),
                    Decoder = Decoder.DumpHfdl,
                    Protocol = Protocol.Tcp,
                    Address = "feed.airframes.io",
                    Port = 5556
                },
                new()
                {
                    Guid = Guid.NewGuid(),
                    Decoder = Decoder.Acarsdec,
                    Protocol = Protocol.Udp,
                    Address = "feed.airframes.io",
                    Port = 5550
                },
                new()
                {
                    Guid = Guid.NewGuid(),
                    Decoder = Decoder.Jaero,
                    Protocol = Protocol.Udp,
                    Address = "feed.airframes.io",
                    Port = 5571
                }
            });
    }
}