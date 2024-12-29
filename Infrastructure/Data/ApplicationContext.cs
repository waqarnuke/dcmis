using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserDetails> UserDetails { get; set; }
}
