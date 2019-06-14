using CambiaUnaVida.Models;
using CambiaUnaVida.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CambiaUnaVida.Controllers
{
    public class ResponsableController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Responsable")]
        // GET: Responsable
        public ActionResult Index()
        {
            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);
            ViewBag.Nombre = usuarioActual.nombre;

            return View(usuarioActual);
        }

        [Authorize(Roles = "Responsable")]
        // GET: Responsable/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            ApplicationUser usu = uS.obtenerPorID(new ApplicationUser { Id = id });

            if (usu == null)
            {
                return HttpNotFound();
            }

            return View(usu);
        }

        [Authorize(Roles = "Responsable")]
        // GET: Responsable/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio uS = new ApplicationUserServicio(db);
            ApplicationUser usu = uS.obtenerPorID(new ApplicationUser { Id = id });

            if (usu == null)
            {
                return HttpNotFound();
            }

            return View(usu);
        }

        [Authorize(Roles = "Responsable")]
        // POST: Responsable/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            ApplicationUser usuarioActual = serv.obtenerPorID(new ApplicationUser { Id = id });

            if (usuarioActual == null)
            {
                return HttpNotFound();
            }

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
    }
}
