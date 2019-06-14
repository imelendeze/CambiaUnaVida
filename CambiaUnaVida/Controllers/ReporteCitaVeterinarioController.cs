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

namespace CambiaUnaVida.Controllers
{
    public class ReporteCitaVeterinarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteCitaVeterinario
        public ActionResult Index()
        {
            var reportesCitasVeterinario = db.ReportesCitasVeterinario.Include(r => r.CitaVeterinarioFK);
            return View(reportesCitasVeterinario.ToList());
        }

        [Authorize(Roles="Adoptante, Veterinario")]
        // GET: ReporteCitaVeterinario/Details/5
        public ActionResult Details(int? idReporte)
        {
            if (idReporte == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReporteCitaVeterinarioServicio rS = new ReporteCitaVeterinarioServicio(db);
            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            ReporteCitaVeterinario reporte = rS.obtenerPorID(new ReporteCitaVeterinario { id= (int)idReporte });

            if(User.IsInRole("Adoptante"))
            {
                ViewBag.Role = "Adoptante";
            }
            else
            {
                ViewBag.Role = "Veterinario";
            }

            ViewBag.NombreAdoptante = uS.obtenerPorID(new ApplicationUser { Id = reporte.CitaVeterinarioFK.idAdoptanteFK }).nombre;

            if (reporte == null)
            {
                return HttpNotFound();
            }

            return View(reporte);
        }

        [Authorize(Roles ="Veterinario")]
        // GET: ReporteCitaVeterinario/Create
        public ActionResult Create(int? idCitaVeterinario)
        {
            ViewBag.idCitaVeterinario = idCitaVeterinario;

            return View();
        }

        // POST: ReporteCitaVeterinario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Veterinario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection c)
        {
            if (ModelState.IsValid)
            {
                ReporteCitaVeterinarioServicio rS = new ReporteCitaVeterinarioServicio(db);
                ReporteCitaVeterinario reporte = new ReporteCitaVeterinario();

                reporte.idCitaVeterinarioFK = int.Parse(Request.Form["idCitaVeterinarioFK"]);
                reporte.observaciones = Request.Form["observaciones"];

                rS.guardar(reporte);

                return RedirectToAction("Index", "Veterinario");
            }

            return View(c);
        }

        // GET: ReporteCitaVeterinario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteCitaVeterinario reporteCitaVeterinario = db.ReportesCitasVeterinario.Find(id);
            if (reporteCitaVeterinario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCitaVeterinarioFK = new SelectList(db.CitasVeterinarios, "id", "idAdoptanteFK", reporteCitaVeterinario.idCitaVeterinarioFK);
            return View(reporteCitaVeterinario);
        }

        // POST: ReporteCitaVeterinario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idCitaVeterinarioFK,observaciones")] ReporteCitaVeterinario reporteCitaVeterinario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporteCitaVeterinario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCitaVeterinarioFK = new SelectList(db.CitasVeterinarios, "id", "idAdoptanteFK", reporteCitaVeterinario.idCitaVeterinarioFK);
            return View(reporteCitaVeterinario);
        }

        // GET: ReporteCitaVeterinario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteCitaVeterinario reporteCitaVeterinario = db.ReportesCitasVeterinario.Find(id);
            if (reporteCitaVeterinario == null)
            {
                return HttpNotFound();
            }
            return View(reporteCitaVeterinario);
        }

        // POST: ReporteCitaVeterinario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReporteCitaVeterinario reporteCitaVeterinario = db.ReportesCitasVeterinario.Find(id);
            db.ReportesCitasVeterinario.Remove(reporteCitaVeterinario);
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
