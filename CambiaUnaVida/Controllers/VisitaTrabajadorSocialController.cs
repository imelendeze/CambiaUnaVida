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
    public class VisitaTrabajadorSocialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles ="TrabajadorSocial, Adoptante")]
        // GET: CitaVeterinario SOLO ENTRA EL ADOPTANTE Y TRABAJADOR
        public ActionResult IndexVisitasTrabajadorPorGato(int? idGato, string idTrabajadorSocial, string idAdoptante)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            VisitaTrabajadorSocialServicio vS = new VisitaTrabajadorSocialServicio(db);
            List<VisitaTrabajadorSocial> visitasUsuario = new List<VisitaTrabajadorSocial>();
            ReporteVisitaTrabajadorSocialServicio rS = new ReporteVisitaTrabajadorSocialServicio(db);
            List<ReporteVisitaTrabajadorSocial> existeReporte = new List<ReporteVisitaTrabajadorSocial>();
            List<VisitaTrabajadorSocial_ReporteViewModel> visitasUsuarioR = new List<VisitaTrabajadorSocial_ReporteViewModel>();

            if (User.IsInRole("TrabajadorSocial"))
            {
                visitasUsuario = vS.obtenerPor(v => v.idTrabajadorSocialFK == usuarioActual.Id && v.idGatoFK == idGato);
                ViewBag.Rol = "TrabajadorSocial";
            }
            else
            {
                visitasUsuario = vS.obtenerPor(v => v.idAdoptanteFK == usuarioActual.Id && v.idGatoFK == idGato);
                ViewBag.Rol = "Adoptante";
            }

            foreach (var item in visitasUsuario)
            {
                VisitaTrabajadorSocial_ReporteViewModel temp = new VisitaTrabajadorSocial_ReporteViewModel();

                temp.id = item.id;
                temp.idAdoptanteFK = item.idAdoptanteFK;
                temp.idGato = item.idGatoFK;
                temp.idTrabajadorSocialFK = item.idTrabajadorSocialFK;
                temp.fecha = item.fecha;
                temp.hora = item.hora;                

                existeReporte = rS.obtenerPor(r => r.idVisitaTrabajadorSocial == item.id);

                if (existeReporte.Count != 0)
                {
                    temp.idReporteVisita = existeReporte.ElementAt(0).id;
                }
                else
                {
                    temp.idReporteVisita = -1;
                }

                visitasUsuarioR.Add(temp);
            }

            @ViewBag.idGato = idGato;
            @ViewBag.idTrabajadorSocial = idTrabajadorSocial;
            @ViewBag.idAdoptante = idAdoptante;

            return View(visitasUsuarioR);
        }

        // GET: VisitaTrabajadorSocial
        public ActionResult Index()
        {
            return View(db.VisitasTrabajadorSocial.ToList());
        }

        // GET: VisitaTrabajadorSocial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitaTrabajadorSocial visitaTrabajadorSocial = db.VisitasTrabajadorSocial.Find(id);
            if (visitaTrabajadorSocial == null)
            {
                return HttpNotFound();
            }
            return View(visitaTrabajadorSocial);
        }

        [Authorize(Roles ="TrabajadorSocial")]
        // GET: VisitaTrabajadorSocial/Create
        public ActionResult Create(int? idGato, string idTrabajadorSocial, string idAdoptante)
        {
            ViewBag.idGato = idGato;
            ViewBag.idTrabajadorSocial = idTrabajadorSocial;
            ViewBag.idAdoptante = idAdoptante;

            return View();
        }

        // POST: VisitaTrabajadorSocial/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "TrabajadorSocial")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection c)
        {
            if (ModelState.IsValid)
            {
                VisitaTrabajadorSocialServicio vS = new VisitaTrabajadorSocialServicio(db);
                VisitaTrabajadorSocial visita = new VisitaTrabajadorSocial();

                visita.idAdoptanteFK = Request.Form["idAdoptanteFK"];
                visita.idTrabajadorSocialFK = Request.Form["idTrabajadorSocialFK"];
                visita.idGatoFK = int.Parse(Request.Form["idGatoFK"]);
                visita.hora = Request.Form["hora"];
                visita.fecha = DateTime.Parse(Request.Form["fecha"]);                

                vS.guardar(visita);

                return RedirectToAction("IndexVisitasTrabajadorPorGato", new { idGato = visita.idGatoFK, idVeterinario = visita.idTrabajadorSocialFK, idAdoptante = visita.idAdoptanteFK });
            }

            return View(c);
        }

        // GET: VisitaTrabajadorSocial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitaTrabajadorSocial visitaTrabajadorSocial = db.VisitasTrabajadorSocial.Find(id);
            if (visitaTrabajadorSocial == null)
            {
                return HttpNotFound();
            }
            return View(visitaTrabajadorSocial);
        }

        // POST: VisitaTrabajadorSocial/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idTrabajadorSocialFK,idAdoptanteFK,idGatoFK,fecha,hora")] VisitaTrabajadorSocial visitaTrabajadorSocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitaTrabajadorSocial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitaTrabajadorSocial);
        }

        // GET: VisitaTrabajadorSocial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitaTrabajadorSocial visitaTrabajadorSocial = db.VisitasTrabajadorSocial.Find(id);
            if (visitaTrabajadorSocial == null)
            {
                return HttpNotFound();
            }
            return View(visitaTrabajadorSocial);
        }

        // POST: VisitaTrabajadorSocial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitaTrabajadorSocial visitaTrabajadorSocial = db.VisitasTrabajadorSocial.Find(id);
            db.VisitasTrabajadorSocial.Remove(visitaTrabajadorSocial);
            db.SaveChanges();
            return RedirectToAction("Index");
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
