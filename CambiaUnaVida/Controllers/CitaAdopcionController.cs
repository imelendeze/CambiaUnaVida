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
    public class CitaAdopcionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CitaAdopcions
        public ActionResult Index()
        {
            return View(db.CitasAdopciones.ToList());
        }

        [Authorize(Roles="Responsable, Adoptante")]
        // GET: CitaAdopcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
            ApplicationUserServicio aS = new ApplicationUserServicio(db);
            CitaAdopcion citaAdopcion = cS.obtenerPorID(new CitaAdopcion { id= (int)id } );

            ViewBag.NombreTrabajador = aS.obtenerPorID(new ApplicationUser { Id= citaAdopcion.idTrabajadorSocialFK }).nombre;

            var usuario = aS.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var rol = userManager.GetRoles(usuarioActual.Id);

            ViewBag.rol = rol.ToList().ElementAt(0);
            ViewBag.IdResponsable = usuarioActual.Id;

            if (citaAdopcion == null)
            {
                return HttpNotFound();
            }

            return View(citaAdopcion);
        }

        [Authorize(Roles = "Responsable")]
        // GET: CitaAdopcions/Create
        public ActionResult Create(int? idPeticionAdopcion)
        {
            if (idPeticionAdopcion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("TrabajadorSocial").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;
            ViewBag.idPeticion = idPeticionAdopcion;
            ViewBag.Trabajadores = new SelectList(usuarios, "Id", "nombre");

            return View();
        }

        // POST: CitaAdopcions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Responsable")]
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
                CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
                CitaAdopcion cita = new CitaAdopcion();

                cita.idPeticionAdopcionFK = int.Parse(Request.Form["idPeticionAdopcionFK"]);
                cita.idTrabajadorSocialFK = Request.Form["trabajadorSocial"];
                cita.fecha = DateTime.Parse(Request.Form["fecha"]);
                cita.hora = Request.Form["hora"];

                cS.guardar(cita);

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);

                return RedirectToAction("IndexGatosDelResponsable", "Gato", new { id= usuarioActual.Id });
            }

            return View(c);
        }

        [Authorize(Roles = "Responsable")]
        // GET: CitaAdopcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
            CitaAdopcion citaAdopcion = cS.obtenerPorID(new CitaAdopcion { id= (int)id });
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("TrabajadorSocial").Users.First();
            usuarios = serv.obtenerPor(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId));            
            ViewBag.Trabajadores = new SelectList(usuarios, "Id", "nombre");
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            if (citaAdopcion == null)
            {
                return HttpNotFound();
            }

            return View(citaAdopcion);
        }

        // POST: CitaAdopcions/Edit/5
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

            CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
            CitaAdopcion cita = cS.obtenerPorID(new CitaAdopcion { id = (int)id });

            if (ModelState.IsValid)
            {                

                cita.idPeticionAdopcionFK = int.Parse(Request.Form["idPeticionAdopcionFK"]);
                cita.idTrabajadorSocialFK = Request.Form["trabajadorSocial"];
                cita.fecha = DateTime.Parse(Request.Form["fecha"]);
                cita.hora = Request.Form["hora"];

                cS.actualizar(cita);                

                return RedirectToAction("Details", new { id = cita.id });
            }

            return View(cita);
        }

        // GET: CitaAdopcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaAdopcion citaAdopcion = db.CitasAdopciones.Find(id);
            if (citaAdopcion == null)
            {
                return HttpNotFound();
            }
            return View(citaAdopcion);
        }

        // POST: CitaAdopcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CitaAdopcion citaAdopcion = db.CitasAdopciones.Find(id);
            db.CitasAdopciones.Remove(citaAdopcion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Adoptante")]
        // GET: CitaAdopcions
        public ActionResult IndexCitaPeticion(int idPeticion)
        {
            CitaAdopcionServicio servCitaPeticion = new CitaAdopcionServicio(db);
            CitaAdopcion_UsuariosViewModel citaPeticion = new CitaAdopcion_UsuariosViewModel();
            List<CitaAdopcion> listaCitas = servCitaPeticion.obtenerPor(s => s.idPeticionAdopcionFK == idPeticion);
            var elementosLista = (from c in listaCitas join usu in db.Users on c.idTrabajadorSocialFK equals usu.Id select new { Id_Cita= c.id, Id_Peticion= c.idPeticionAdopcionFK, Id_TrabajadorSocial= usu.Id, Nombre_TrabajadorSocial= usu.nombre, Fecha= c.fecha, Hora= c.hora }).ToList();
            CitaAdopcion_UsuariosViewModel cita = new CitaAdopcion_UsuariosViewModel();

            foreach(var item in elementosLista)
            {
                cita.idCitaAdopcion = item.Id_Cita;
                cita.idPeticionAdopcion = item.Id_Peticion;
                cita.idTrabajadorSocial = item.Id_TrabajadorSocial;
                cita.nombreTrabajadorSocial = item.Nombre_TrabajadorSocial;
                cita.fecha = item.Fecha;
                cita.hora = item.Hora;
            }

            return View(cita);
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
