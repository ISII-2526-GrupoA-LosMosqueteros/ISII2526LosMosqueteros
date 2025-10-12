using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    [Display(Name = "Nombre")]
    public string Name { get; set; }

    [Display(Name = "Apellidos")]
    public string Surname { get; set; }

    [Display(Name = "Correo Electronico")]
    public string Email { get; set; }

    [Display(Name = "Telefono")]
    public string Phone { get; set; }

    public IList<Reparacion> Reparaciones { get; set; }
    public IList<Compra> Compras { get; set; }
}