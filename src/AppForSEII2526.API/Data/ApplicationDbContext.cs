using AppForSEII2526.API.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<AlquilarItem>().HasKey(pi => new { pi.IdAlquiler, pi.IdHerramienta });
    }


    public DbSet<Alquiler> Alquileres { get; set; }
    public DbSet<AlquilarItem> AlquilarItems { get; set; }
    public DbSet<Herramienta> Herramientas { get; set; }
    public DbSet<Fabricante> Fabricantes { get; set; }

}
