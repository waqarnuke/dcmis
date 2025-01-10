using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CardDetail>? CardDetails { get; set; }
    public DbSet<Core.Entities.Photo>? Photos { get; set; }
    public DbSet<Core.Entities.QRCode>? QRCodes { get; set; }
}
