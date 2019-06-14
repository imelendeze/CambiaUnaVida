using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CambiaUnaVida.Models;
using CambiaUnaVida.Models.Dominio;
using CambiaUnaVida.Services;
using CambiaUnaVida.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CambiaUnaVida.Controllers
{
    public class AdopcionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();        

        [Authorize(Roles= "Responsable")]
        // GET: Adopcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdopcionServicio aS = new AdopcionServicio(db);
            Adopcion adopcion = aS.obtenerPorID(new Adopcion { id= (int)id });

            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            var usuario = uS.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            ViewBag.NombreAdoptante = uS.obtenerPorID(new ApplicationUser { Id= adopcion.PeticionAdopcionFK.idAdoptanteFK }).nombre;
            ViewBag.NombreTrabajador = uS.obtenerPorID(new ApplicationUser { Id = adopcion.idTrabajadorSocialFK }).nombre;
            ViewBag.NombreVeterinario = uS.obtenerPorID(new ApplicationUser { Id = adopcion.idVeterinarioFK }).nombre;

            if (adopcion == null)
            {
                return HttpNotFound();
            }
            return View(adopcion);
        }

        [Authorize(Roles= "Responsable")]
        // GET: Adopcions/Create
        public ActionResult Create(int? idPeticion)
        {
            if (idPeticion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("TrabajadorSocial").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));
            
            ViewBag.Trabajadores = new SelectList(usuarios, "Id", "nombre");

            role= roleManager.FindByName("Veterinario").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));

            ViewBag.Veterinario = new SelectList(usuarios, "Id", "nombre");

            ViewBag.idPeticion = idPeticion;

            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            return View();
        }

        // POST: Adopcions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles="Responsable")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection c)
        {
            if(c==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                AdopcionServicio aS = new AdopcionServicio(db);
                Adopcion ado = new Adopcion();

                ado.idPeticionAdopcionFK = int.Parse(Request.Form["idPeticionAdopcionFK"]);
                ado.idTrabajadorSocialFK = Request.Form["trabajadorSocial"];
                ado.idVeterinarioFK = Request.Form["veterinario"];
                ado.fecha = DateTime.Parse(Request.Form["fecha"]);
                ado.hora = Request.Form["hora"];

                aS.guardar(ado);

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);                

                return RedirectToAction("IndexGatosDelResponsable", "Gato", new { id= usuarioActual.Id });
            }
            
            return View(c);
        }

        [Authorize(Roles = "Responsable")]
        // GET: Adopcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdopcionServicio aS = new AdopcionServicio(db);
            Adopcion ado = aS.obtenerPorID(new Adopcion { id= (int)id } );

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("TrabajadorSocial").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));

            ViewBag.Trabajadores = new SelectList(usuarios, "Id", "nombre");

            role = roleManager.FindByName("Veterinario").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));

            ViewBag.Veterinario = new SelectList(usuarios, "Id", "nombre");

            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            if (ado == null)
            {
                return HttpNotFound();
            }
            
            return View(ado);
        }

        // POST: Adopcions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Responsable")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection c, int? id)
        {
            if(c==null||id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                AdopcionServicio aS = new AdopcionServicio(db);
                Adopcion ado = aS.obtenerPorID(new Adopcion { id= (int)id });

                ado.idPeticionAdopcionFK = int.Parse(Request.Form["idPeticionAdopcionFK"]);
                ado.idTrabajadorSocialFK = Request.Form["trabajadorSocial"];
                ado.idVeterinarioFK = Request.Form["veterinario"];
                ado.fecha = DateTime.Parse(Request.Form["fecha"]);
                ado.hora = Request.Form["hora"];

                aS.actualizar(ado);

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);

                return RedirectToAction("Details", new { id=ado.id });
            }
            
            return View(c);
        }        

        [Authorize(Roles="Adoptante")]
        public ActionResult IndexAdopcionesAdoptante()
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            AdopcionServicio aS = new AdopcionServicio(db);
            PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
            List<Adopcion_Usuarios_GatoViewModel> adopcionesUsuario = new List<Adopcion_Usuarios_GatoViewModel>();
            GatoServicio gS = new GatoServicio(db);

            List<PeticionAdopcion> peticionesAceptadas = pS.obtenerPor(p => p.idAdoptanteFK==usuarioActual.Id && p.status=="Aceptada");

            if(peticionesAceptadas.Count!=0)
            {
                var aU= (from pA in peticionesAceptadas join adop in db.Adopciones on pA.id equals adop.idPeticionAdopcionFK select new { Id_Adopcion= adop.id, Id_PeticionAdopcion= adop.idPeticionAdopcionFK, Id_TrabajadorSocial= adop.idTrabajadorSocialFK, Id_Veterinario= adop.idVeterinarioFK, Fecha= adop.fecha, Hora= adop.hora, Id_Gato= pA.idGatoFK }).ToList();

                foreach(var item in aU)
                {
                    Adopcion_Usuarios_GatoViewModel temp = new Adopcion_Usuarios_GatoViewModel();

                    temp.idAdopcion = item.Id_Adopcion;
                    temp.idPeticionAdopcionFK = item.Id_PeticionAdopcion;
                    temp.idTrabajadorSocialFK = item.Id_TrabajadorSocial;
                    temp.idVeterinarioFK = item.Id_Veterinario;
                    temp.fecha = item.Fecha;
                    temp.hora = item.Hora;
                    temp.idGatoFK = item.Id_Gato;
                    temp.nombreVeterinario = serv.obtenerPorID(new ApplicationUser { Id= item.Id_Veterinario }).nombre;
                    temp.nombreTrabajador = serv.obtenerPorID(new ApplicationUser { Id = item.Id_TrabajadorSocial }).nombre;
                    temp.nombreGato = gS.obtenerPorID(new Gato { id= item.Id_Gato }).nombre;
                    temp.idAdoptante = usuarioActual.Id;

                    adopcionesUsuario.Add(temp);
                }
            }

            return View(adopcionesUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
