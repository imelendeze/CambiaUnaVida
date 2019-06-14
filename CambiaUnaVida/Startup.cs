using CambiaUnaVida.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CambiaUnaVida.Startup))]
namespace CambiaUnaVida
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            crearRoles();
        }

        public void crearRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Iván: Si no existe el rol de admin, lo creamos.
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();

                role.Name = "Admin";
                roleManager.Create(role);

                //Creamos el administrador del sistema
                var user = new ApplicationUser();

                user.UserName = "imelendezenriquez@gmail.com";
                user.nombre = "Iván";
                user.apellidos = "Meléndez";

                string contraseña = "123456";

                var checarUsu = userManager.Create(user, contraseña);

                if(checarUsu.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            //Iván: Si no existe el rol de administrador del albergue, lo creamos.
            if(!roleManager.RoleExists("AdminAlbergue"))
            {
                var role = new IdentityRole();

                role.Name = "AdminAlbergue";
                roleManager.Create(role);
            }

            //Iván: Si no existe el rol de responsable de un gato, lo creamos.
            if (!roleManager.RoleExists("Responsable"))
            {
                var role = new IdentityRole();

                role.Name = "Responsable";
                roleManager.Create(role);
            }

            //Iván: Si no existe el rol de adoptante, lo creamos.
            if (!roleManager.RoleExists("Adoptante"))
            {
                var role = new IdentityRole();

                role.Name = "Adoptante";
                roleManager.Create(role);
            }

            //Iván: Si no existe el rol de trabajador social, lo creamos.
            if (!roleManager.RoleExists("TrabajadorSocial"))
            {
                var role = new IdentityRole();

                role.Name = "TrabajadorSocial";
                roleManager.Create(role);
            }

            //Iván: Si no existe el rol de veterinario, lo creamos.
            if (!roleManager.RoleExists("Veterinario"))
            {
                var role = new IdentityRole();

                role.Name = "Veterinario";
                roleManager.Create(role);
            }
        }
    }    
}
