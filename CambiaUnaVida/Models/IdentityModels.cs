using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CambiaUnaVida.Models.Dominio;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CambiaUnaVida.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        //Iván: Mis propiedades para los usuarios: Responsable, Administrador del albergue, Veterinario, Trabajador social y Adoptante. Para el administrador del sistema solo basta su email y contraseña.        
        [StringLength(20)]
        public string nombre { get; set; }
        [StringLength(30)]
        public string apellidos { get; set; }
        [StringLength(80)]
        public string direccion { get; set; }
        [StringLength(20)]
        public string telefono { get; set; }
        [StringLength(20)]
        public string sexo { get; set; }
        public int edad { get; set; }
        [StringLength(30)]
        public  string ocupacion { get; set; }
        [StringLength(20)]
        public string nombreReferencia { get; set; }
        [StringLength(30)]
        public string apellidosReferencia { get; set; }
        [StringLength(20)]
        public string telefonoReferencia { get; set; }
        [StringLength(80)]
        public string direccionReferencia { get; set; }
        [StringLength(30)]
        public string emailReferencia { get; set; }
        [StringLength(30)]
        public string cedulaProfesional { get; set; }        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("conexionBD", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }        

        public DbSet<Adopcion> Adopciones { get; set; }
        public DbSet<CitaAdopcion> CitasAdopciones { get; set; }
        public DbSet<CitaVeterinario> CitasVeterinarios { get; set; }
        public DbSet<Gato> Gatos { get; set; }
        public DbSet<PeticionAdopcion> PeticionesAdopcion { get; set; }
        public DbSet<ReporteCitaVeterinario> ReportesCitasVeterinario { get; set; }
        public DbSet<ReporteVisitaTrabajadorSocial> ReportesVisitasTrabajadorSocial { get; set; }
        public DbSet<VisitaTrabajadorSocial> VisitasTrabajadorSocial { get; set; }
        public DbSet<CitaVeterinario_Usuarios> CitasVeterinario_Usuarios { get; set; }
        public DbSet<Adopcion_Usuarios> Adopciones_Usuarios { get; set; }
        public DbSet<VisitaTrabajadorSocial_Usuarios> VisitasTrabajadorSocial_Usuarios { get; set; }        
    }
}