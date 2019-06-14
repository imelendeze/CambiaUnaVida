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
    public class ReporteVisitaTrabajadorSocialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();        

        [Authorize(Roles ="TrabajadorSocial, Adoptante")]
        // GET: ReporteVisitaTrabajadorSocial/Details/5
        public ActionResult Details(int? idReporte)
        {
            if (idReporte == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReporteVisitaTrabajadorSocialServicio rS = new ReporteVisitaTrabajadorSocialServicio(db);
            ReporteVisitaTrabajadorSocial reporte = rS.obtenerPorID(new ReporteVisitaTrabajadorSocial { id = (int)idReporte });

            if(User.IsInRole("TrabajadorSocial"))
            {
                ViewBag.Rol = "TrabajadorSocial";
            }
            else
            {
                ViewBag.Rol = "Adoptante";
            }

            if (reporte == null)
            {
                return HttpNotFound();
            }
            return View(reporte);
        }

        [Authorize(Roles ="TrabajadorSocial")]
        // GET: ReporteVisitaTrabajadorSocial/Create
        public ActionResult Create(int? idVisitaTrabajadorSocial)
        {
            ViewBag.idVisitaTrabajadorSocial = idVisitaTrabajadorSocial;

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            ViewBag.IdTrabajadorSocial = usuarioActual.Id;


            return View();
        }

        // POST: ReporteVisitaTrabajadorSocial/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "TrabajadorSocial")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection c)
        {
            if (ModelState.IsValid)
            {
                ReporteVisitaTrabajadorSocialServicio rS = new ReporteVisitaTrabajadorSocialServicio(db);
                ReporteVisitaTrabajadorSocial reporte = new ReporteVisitaTrabajadorSocial();

                reporte.idVisitaTrabajadorSocial = int.Parse(Request.Form["idVisitaTrabajadorSocial"]);
                reporte.observaciones = Request.Form["observaciones"];

                rS.guardar(reporte);

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);

                return RedirectToAction("IndexGatosTrabajadorSocial", "Gato", new { id= usuarioActual.Id });
            }

            return View(c);
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
