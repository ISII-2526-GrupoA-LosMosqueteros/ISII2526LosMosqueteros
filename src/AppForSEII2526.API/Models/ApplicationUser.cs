using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    public ApplicationUser()
    {
    }

    public ApplicationUser(string name, string surname, string? email, string? phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

    [Display(Name = "Nombre")]
    public string Name { get; set; }

    [Display(Name = "Apellidos")]
    public string Surname { get; set; }

    [Display(Name = "Correo Electronico")]
    public string? Email { get; set; }

    [Display(Name = "Telefono")]
    public string? Phone { get; set; }

    public IList<Reparacion> Reparaciones { get; set; }
    public IList<Compra> Compras { get; set; }
    public IList<Alquiler> Alquileres { get; set; }
    public IList<Oferta> Ofertas { get; set; }

}