using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {


    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<CompraItem>().HasKey(pi => new { pi.IdCompra, pi.IdHerramienta });
    }

    public DbSet<Fabricante> Fabricantes { get; set; }
    public DbSet<Herramienta> Herramientas { get; set; }
    public DbSet<Compra> Compra { get; set; }
    public DbSet<CompraItem> CompraItems { get; set; }

}
