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
    public class CitaVeterinarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [Authorize(Roles="Adoptante, Veterinario")]
        // GET: CitaVeterinario SOLO ENTRA EL ADOPTANTE Y VETERINARIO
        public ActionResult IndexCitasVeterinarioPorGato(int? idGato, string idVeterinario, string idAdoptante)
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            CitaVeterinarioServicio vS = new CitaVeterinarioServicio(db);
            List<CitaVeterinario> citasUsuario = new List<CitaVeterinario>();
            ReporteCitaVeterinarioServicio rS = new ReporteCitaVeterinarioServicio(db);
            List<ReporteCitaVeterinario> existeReporte = new List<ReporteCitaVeterinario>();
            List<CitaVeterinario_ReporteViewModel> citasUsuarioR = new List<CitaVeterinario_ReporteViewModel>();

            if(User.IsInRole("Veterinario"))
            {
                citasUsuario = vS.obtenerPor(v => v.idVeterinarioFK == usuarioActual.Id && v.idGato == idGato);
                ViewBag.Rol = "Veterinario";
            }
            else
            {
                citasUsuario = vS.obtenerPor(v => v.idAdoptanteFK == usuarioActual.Id && v.idGato == idGato);
                ViewBag.Rol = "Adoptante";
            }

            foreach(var item in citasUsuario)
            {
                CitaVeterinario_ReporteViewModel temp = new CitaVeterinario_ReporteViewModel();

                temp.id = item.id;
                temp.idAdoptanteFK = item.idAdoptanteFK;
                temp.idGato = item.idGato;
                temp.idVeterinarioFK = item.idVeterinarioFK;
                temp.fecha = item.fecha;
                temp.hora = item.hora;
                temp.status = item.status;

                existeReporte = rS.obtenerPor(r=> r.idCitaVeterinarioFK == item.id);

                if(existeReporte.Count!=0)
                {
                    temp.idReporteCita = existeReporte.ElementAt(0).id;
                }
                else
                {
                    temp.idReporteCita = -1;
                }

                citasUsuarioR.Add(temp);
            }

            @ViewBag.idGato = idGato;
            @ViewBag.idVeterinario = idVeterinario;
            @ViewBag.idAdoptante = idAdoptante;

            return View(citasUsuarioR);
        }

        // GET: CitaVeterinario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaVeterinario citaVeterinario = db.CitasVeterinarios.Find(id);
            if (citaVeterinario == null)
            {
                return HttpNotFound();
            }
            return View(citaVeterinario);
        }

        [Authorize(Roles="Adoptante")]
        // GET: CitaVeterinario/Create
        public ActionResult Create(int? idGato, string idVeterinario, string idAdoptante)
        {
            ViewBag.idGato = idGato;
            ViewBag.idVeterinario = idVeterinario;
            ViewBag.idAdoptante = idAdoptante;

            return View();
        }

        // POST: CitaVeterinario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Adoptante")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection c)
        {
            if (ModelState.IsValid)
            {
                CitaVeterinarioServicio cS = new CitaVeterinarioServicio(db);
                CitaVeterinario cita = new CitaVeterinario();

                cita.idAdoptanteFK = Request.Form["idAdoptanteFK"];
                cita.idVeterinarioFK = Request.Form["idVeterinarioFK"];
                cita.idGato = int.Parse(Request.Form["idGato"]);
                cita.hora = Request.Form["hora"];
                cita.fecha = DateTime.Parse(Request.Form["fecha"]);
                cita.status = "Por confirmar";

                cS.guardar(cita);

                return RedirectToAction("IndexCitasVeterinarioPorGato", new { idGato= cita.idGato, idVeterinario= cita.idVeterinarioFK, idAdoptante = cita.idAdoptanteFK });
            }

            return View(c);
        }

        // GET: CitaVeterinario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaVeterinario citaVeterinario = db.CitasVeterinarios.Find(id);
            if (citaVeterinario == null)
            {
                return HttpNotFound();
            }
            return View(citaVeterinario);
        }

        // POST: CitaVeterinario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idAdoptanteFK,idVeterinarioFK,idGato,fecha,hora,status")] CitaVeterinario citaVeterinario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citaVeterinario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(citaVeterinario);
        }       

        [Authorize(Roles="Veterinario")]
        public ActionResult actualizarEstadoCita(int? id, string estado)
        {            
            CitaVeterinarioServicio cS = new CitaVeterinarioServicio(db);
            CitaVeterinario cita = cS.obtenerPorID(new CitaVeterinario { id = (int)id });

            if(estado=="Confirmar")
            {
                cita.status = "Confirmada";
            }
            else
            {
                cita.status = "Rechazada";
            }

            cS.actualizar(cita);

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);            

            return RedirectToAction("IndexGatosVeterinarios", "Gato", new { id=  usuarioActual.Id} );
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
