using CambiaUnaVida.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CambiaUnaVida.Services;
using CambiaUnaVida.ViewModels;
using CambiaUnaVida.Models.Dominio;
using System.Net;

namespace CambiaUnaVida.Controllers
{
    public class AdminAlbergueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();     

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue
        public ActionResult Index()
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);  
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            ViewBag.Nombre = usuarioActual.nombre;

            return View(usuarioActual);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue
        public ActionResult IndexUsuarios(string rol)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            ViewBag.Rol = rol;

            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var role = roleManager.FindByName(rol).Users.First();
                usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));
            }
            catch (Exception e)
            {
            }

            return View(usuarios);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            ApplicationUser usu = uS.obtenerPorID(new ApplicationUser { Id= id });

            if(usu==null)
            {
                return HttpNotFound();
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var rol = userManager.GetRoles(usu.Id);

            ViewBag.rol = rol.ToList().ElementAt(0);

            return View(usu);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Create
        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(db.Roles.Where(r => !r.Name.Equals("Admin") && !r.Name.Equals("Adoptante")).ToList(), "Name", "Name");

            return View();
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // POST: AdminAlbergue/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, nombre = model.Nombre, apellidos = model.Apellidos, direccion = model.Direccion, telefono = model.Telefono, sexo = model.Sexo, edad = model.Edad, ocupacion = model.Ocupacion, nombreReferencia = model.NombreReferencia, apellidosReferencia = model.ApellidosReferencia, telefonoReferencia = model.TelefonoReferencia, direccionReferencia = model.DireccionReferencia, emailReferencia = model.EmailReferencia, cedulaProfesional = model.CedulaProfesional };

                var result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                    // Enviar correo electrónico con este vínculo
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                    var result2 = userManager.AddToRole(user.Id, model.RolesUsuario);

                    return RedirectToAction("Index", "AdminAlbergue");
                }

                ViewBag.Name = new SelectList(db.Roles.Where(r => !r.Name.Equals("Admin") && !r.Name.Equals("Adoptante")).ToList(), "Name", "Name");

                AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Create
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            ApplicationUser usu = uS.obtenerPorID(new ApplicationUser { Id= id });

            if(usu==null)
            {
                return HttpNotFound();
            }

            return View(usu);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // POST: AdminAlbergue/Create
        [HttpPost]
        public ActionResult Edit(FormCollection collection, string id)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            ViewBag.Nombre = usuarioActual.nombre;

            if (ModelState.IsValid)
            {
                usuarioActual.nombre = Request.Form["nombre"];
                usuarioActual.apellidos = Request.Form["apellidos"];
                usuarioActual.direccion = Request.Form["direccion"];
                usuarioActual.telefono = Request.Form["telefono"];
                usuarioActual.sexo = Request.Form["sexo"];
                usuarioActual.edad = int.Parse(Request.Form["edad"]);

                serv.actualizar(usuarioActual);

                return RedirectToAction("Index");
            }

            return View(usuarioActual);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /*******************************************Acciones para gatos**********************************************/
        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue
        public ActionResult IndexGatosAdminAlbergue()
        {
            List<Gato_ResponsableViewModel> listaGatos = new List<Gato_ResponsableViewModel>();
            var elementosLista = (from usu in db.Users join ga in db.Gatos on usu.Id equals ga.idResponsableFK select new { Id_Responsable = usu.Id, Nombre_Responsable = usu.nombre, Id_Gato = ga.id, Nombre_Gato = ga.nombre, Edad = ga.edad, Sexo = ga.sexo, Foto = ga.foto, Observaciones = ga.observaciones, Padecimientos = ga.padecimientos, Dieta = ga.dieta, Status = ga.status }).ToList();

            foreach (var item in elementosLista)
            {
                Gato_ResponsableViewModel objeto = new Gato_ResponsableViewModel();

                objeto.idResponsable = item.Id_Responsable;
                objeto.idGato = item.Id_Gato;
                objeto.nombreResponsable = item.Nombre_Responsable;
                objeto.nombreGato = item.Nombre_Gato;
                objeto.edad = item.Edad;
                objeto.sexo = item.Sexo;
                objeto.foto = item.Foto;
                objeto.observaciones = item.Observaciones;
                objeto.padecimientos = item.Padecimientos;
                objeto.dieta = item.Dieta;
                objeto.status = item.Status;

                listaGatos.Add(objeto);
            }

            return View(listaGatos);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Create
        public ActionResult CreateCat()
        {
            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            List<string> estadosGato = new List<string>();
            ApplicationUserServicio serv = new ApplicationUserServicio(db);  

            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var role = roleManager.FindByName("Responsable").Users.First();
                usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));

                ViewBag.Name = new SelectList(usuarios, "Id", "nombre");

                estadosGato.Add("En adopción");
                estadosGato.Add("En proceso");
                estadosGato.Add("Adoptado");

                ViewBag.Estados = new SelectList(estadosGato);
            }
            catch (Exception e)
            {
            }

            return View();
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCat(FormCollection ent)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            GatoServicio GatoServ = new GatoServicio(db);

            if (ModelState.IsValid)
            {
                Gato g = new Gato();
                ApplicationUser usu = serv.obtenerPorID(new ApplicationUser { Id= Request.Form["responsableGato"]});
                
                g.idResponsableFK = Request.Form["responsableGato"];
                g.nombre = Request.Form["nombre"];
                g.edad = Request.Form["edad"];
                g.sexo = Request.Form["sexo"];
                g.foto = Request.Form["foto"];
                g.observaciones = Request.Form["observaciones"];
                g.padecimientos = Request.Form["padecimientos"];
                g.dieta = Request.Form["dieta"];
                g.status = Request.Form["status"];
                g.ResponsableFK = usu;

                GatoServ.guardar(g);

                return RedirectToAction("IndexGatosAdminAlbergue");
            }

            return View(ent);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Edit
        public ActionResult EditCat(int? idG)
        {
            GatoServicio Gs = new GatoServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();            
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            SelectList usuariosResponsables;

            if (idG == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gato cat = Gs.obtenerPorID(new Gato { id = (int)idG });

            if (cat == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("Responsable").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));

            usuariosResponsables= new SelectList(usuarios, "Id", "nombre");

            foreach (var item in usuariosResponsables)
            {
                if(item.Value==cat.ResponsableFK.Id)
                {
                    item.Selected = true;
                    break;
                }
            }            

            ViewBag.Name = usuariosResponsables;

            return View(cat);            
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // POST: AdminAlbergue/Edit
        [HttpPost]
        public ActionResult EditCat(FormCollection collection)
        {
            GatoServicio gS = new GatoServicio(db);
            ApplicationUserServicio serv = new ApplicationUserServicio(db);

            if (ModelState.IsValid)
            {
                Gato g = gS.obtenerPorID(new Gato { id = int.Parse(Request.Form["id"]) });
                ApplicationUser usu = serv.obtenerPorID(new ApplicationUser { Id = Request.Form["responsableGato"] });

                g.idResponsableFK = Request.Form["responsableGato"];
                g.nombre = Request.Form["nombre"];
                g.edad = Request.Form["edad"];
                g.sexo = Request.Form["sexo"];
                g.foto = Request.Form["foto"];
                g.observaciones = Request.Form["observaciones"];
                g.padecimientos = Request.Form["padecimientos"];
                g.dieta = Request.Form["dieta"];                
                g.ResponsableFK = usu;

                gS.actualizar(g);

                return RedirectToAction("IndexGatosAdminAlbergue");
            }

            return View(collection);
        }

        [Authorize(Roles = ("AdminAlbergue, Admin"))]
        // GET: AdminAlbergue/Details
        public ActionResult DetailsCat(Gato_ResponsableViewModel g)
        {            
            return View(g);
        }
    }
}
