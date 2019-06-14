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

namespace CambiaUnaVida.Controllers
{
    public class PeticionAdopcionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PeticionAdopcion
        public ActionResult Index()
        {
            return View(db.PeticionesAdopcion.ToList());
        }

        [Authorize(Roles = "Responsable")]
        // GET: PeticionAdopcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
            ApplicationUserServicio aS = new ApplicationUserServicio(db);
            GatoServicio gS = new GatoServicio(db);
            PeticionAdopcion p = pS.obtenerPorID(new PeticionAdopcion { id = (int)id });

            ViewBag.NombreAdoptante = aS.obtenerPorID(new ApplicationUser { Id = p.idAdoptanteFK }).nombre;
            ViewBag.NombreGato = gS.obtenerPorID(new Gato { id= p.idGatoFK }).nombre;
            
            var usuario = aS.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // GET: PeticionAdopcion/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: PeticionAdopcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idAdoptanteFK,idGatoFK,fecha,status,observaciones")] PeticionAdopcion peticionAdopcion)
        {
            if (ModelState.IsValid)
            {
                db.PeticionesAdopcion.Add(peticionAdopcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(peticionAdopcion);
        }

        [Authorize(Roles ="Responsable")]
        // GET: PeticionAdopcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
            PeticionAdopcion p = pS.obtenerPorID(new PeticionAdopcion { id= (int)id });

            ApplicationUserServicio aS = new ApplicationUserServicio(db);
            var usuario = aS.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.IdResponsable = usuarioActual.Id;

            if (p == null)
            {
                return HttpNotFound();
            }

            return View(p);
        }

        // POST: PeticionAdopcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Responsable")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection c, int? id)
        {
            if(id==null||c==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
                    PeticionAdopcion p = pS.obtenerPorID(new PeticionAdopcion { id = (int)id });

                    if(p==null)
                    {
                        return HttpNotFound();
                    }

                    p.status = Request.Form["status"];
                    p.observaciones = Request.Form["observaciones"];

                    pS.actualizar(p);

                    return RedirectToAction("Details", new { id= p.id });
                }
            }            

            return View(c);
        }

        // GET: PeticionAdopcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeticionAdopcion peticionAdopcion = db.PeticionesAdopcion.Find(id);
            if (peticionAdopcion == null)
            {
                return HttpNotFound();
            }
            return View(peticionAdopcion);
        }

        // POST: PeticionAdopcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PeticionAdopcion peticionAdopcion = db.PeticionesAdopcion.Find(id);
            db.PeticionesAdopcion.Remove(peticionAdopcion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Adoptante")]
        public ActionResult agregarPeticion(int idGato)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            PeticionAdopcion peticion = new PeticionAdopcion();
            PeticionAdopcionServicio servPeticion = new PeticionAdopcionServicio(db);

            peticion.idAdoptanteFK = usuarioActual.Id;
            peticion.fecha = DateTime.Today;
            peticion.idGatoFK = idGato;
            peticion.status = "Esperando cita";

            servPeticion.guardar(peticion);

            return RedirectToAction("IndexGatosEnAdopcion", "Gato");

        }

        [Authorize(Roles= "Adoptante")]
        public ActionResult IndexPeticionesAdopcion()
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            PeticionAdopcionServicio servPeticion = new PeticionAdopcionServicio(db);
            List<PeticionAdopcion> peticionesUsuario= servPeticion.obtenerPor(p => p.idAdoptanteFK == usuarioActual.Id);
            List<PeticionAdopcion_UsuariosViewModel> listaPeticiones = new List<PeticionAdopcion_UsuariosViewModel>();
            var elementosLista = (from p in peticionesUsuario join g in db.Gatos on p.idGatoFK equals g.id select new { IdPeticion= p.id, Id_Gato = g.id, Nombre_Gato = g.nombre, Fecha = p.fecha, Status = p.status, Observaciones = p.observaciones }).ToList();
            CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
            List<CitaAdopcion> citasAdopcion;

            peticionesUsuario.Clear();

            foreach(var item in elementosLista)
            {
                PeticionAdopcion_UsuariosViewModel p = new PeticionAdopcion_UsuariosViewModel();

                p.id = item.IdPeticion;
                p.idGatoFK = item.Id_Gato;
                p.nombreGato = item.Nombre_Gato;
                p.fecha = item.Fecha;
                p.status = item.Status;
                p.observaciones = item.Observaciones;

                citasAdopcion = cS.obtenerPor(c => c.idPeticionAdopcionFK==p.id);

                if(citasAdopcion.Count!=0)
                {
                    p.idCitaAdopcion = citasAdopcion.ElementAt(0).id;
                }
                else
                {
                    p.idCitaAdopcion = -1;
                }

                listaPeticiones.Add(p);
            }

            return View(listaPeticiones);
        }

        [Authorize(Roles="Adoptante")]
        // GET: PeticionAdopcion/Details/5
        public ActionResult DetallesPeticion(PeticionAdopcion_UsuariosViewModel peticion)
        {
            if (peticion == null)
            {
                return HttpNotFound();
            }
                        
            return View(peticion);
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
