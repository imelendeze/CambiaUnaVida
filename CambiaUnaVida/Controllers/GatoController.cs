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
    public class GatoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Gato Gato { get; private set; }

        [Authorize(Roles= "Admin, AdminAlbergue")]
        // GET: Gato
        public ActionResult Index()
        {
            return View(db.Gatos.ToList());
        }

        [Authorize(Roles = "TrabajadorSocial")]
        public ActionResult IndexGatosTrabajadorSocial(string id)
        {
            if (id != null)
            {
                List<Adopcion_TrabajadorSocialViewModel> gatosDelTrabajadorSocial = new List<Adopcion_TrabajadorSocialViewModel>();
                AdopcionServicio aS = new AdopcionServicio(db);
                List<Adopcion> adopcionesDelTrabajadorSocial = aS.obtenerPor(a => a.idTrabajadorSocialFK == id);
                PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
                GatoServicio gS = new GatoServicio(db);
                ApplicationUserServicio uS = new ApplicationUserServicio(db);

                foreach (var item in adopcionesDelTrabajadorSocial)
                {
                    Adopcion_TrabajadorSocialViewModel temp = new Adopcion_TrabajadorSocialViewModel();

                    temp.idAdopcion = item.id;
                    temp.idPeticionAdopcion = item.idPeticionAdopcionFK;
                    temp.idTrabajadorSocial = item.idTrabajadorSocialFK;

                    PeticionAdopcion p = pS.obtenerPorID(new PeticionAdopcion { id = item.idPeticionAdopcionFK });

                    temp.idAdoptante = p.idAdoptanteFK;
                    temp.idGato = p.idGatoFK;

                    Gato g = gS.obtenerPorID(new Gato { id = p.idGatoFK });

                    temp.nombreGato = g.nombre;

                    ApplicationUser u = uS.obtenerPorID(new ApplicationUser { Id = p.idAdoptanteFK });

                    temp.nombreAdoptante = u.nombre;

                    gatosDelTrabajadorSocial.Add(temp);
                }

                return View(gatosDelTrabajadorSocial);
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "Veterinario")]
        public ActionResult IndexGatosVeterinarios(string id)
        {
            if (id != null)
            {
                List<Adopcion_VeterinarioViewModel> gatosDelVeterinario = new List<Adopcion_VeterinarioViewModel>();
                AdopcionServicio aS = new AdopcionServicio(db);
                List<Adopcion> adopcionesDelVeterinario = aS.obtenerPor(a => a.idVeterinarioFK == id);
                PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
                GatoServicio gS = new GatoServicio(db);
                ApplicationUserServicio uS = new ApplicationUserServicio(db);

                foreach(var item in adopcionesDelVeterinario)
                {
                    Adopcion_VeterinarioViewModel temp = new Adopcion_VeterinarioViewModel();

                    temp.idAdopcion = item.id;
                    temp.idPeticionAdopcion = item.idPeticionAdopcionFK;
                    temp.idVeterinario = item.idVeterinarioFK;

                    PeticionAdopcion p = pS.obtenerPorID( new PeticionAdopcion { id= item.idPeticionAdopcionFK } );

                    temp.idAdoptante = p.idAdoptanteFK;
                    temp.idGato = p.idGatoFK;

                    Gato g = gS.obtenerPorID(new Gato { id = p.idGatoFK });

                    temp.nombreGato = g.nombre;

                    ApplicationUser u = uS.obtenerPorID( new ApplicationUser { Id= p.idAdoptanteFK } );

                    temp.nombreAdoptante = u.nombre;
       
                    gatosDelVeterinario.Add(temp);
                }

                return View(gatosDelVeterinario);
            }

            return HttpNotFound();
        }

        [Authorize(Roles= "Adoptante")]
        // GET: Gato
        public ActionResult IndexGatosEnAdopcion()
        {
            GatoServicio gS = new GatoServicio(db);

            return View(gS.obtenerPor(g => g.status == "En Adopción"));
        }

        [Authorize(Roles = "Responsable")]
        // GET: Gato
        public ActionResult IndexGatosDelResponsable(string id)
        {
            if(id!=null)
            {
                GatoServicio gS = new GatoServicio(db);
                PeticionAdopcionServicio pS = new PeticionAdopcionServicio(db);
                CitaAdopcionServicio cS = new CitaAdopcionServicio(db);
                List<Gato> gatosDelResponsable = gS.obtenerPor(g => g.idResponsableFK == id);
                List<Responsable_GatoPeticionCitaAdopcionViewModel> gatos = new List<Responsable_GatoPeticionCitaAdopcionViewModel>();
                AdopcionServicio aS = new AdopcionServicio(db);
                List<Adopcion> existeAdopcionGato;

                foreach (var item in gatosDelResponsable)
                {
                    Responsable_GatoPeticionCitaAdopcionViewModel g = new Responsable_GatoPeticionCitaAdopcionViewModel();
                    List<PeticionAdopcion> peticionesGato = pS.obtenerPor(p => p.idGatoFK == item.id && p.status!="Rechazada").ToList();

                    if (peticionesGato.Count!=0)
                    {                        
                        g.idPeticionAdopcion = peticionesGato.ElementAt(0).id;
                        g.estadoPeticion = peticionesGato.ElementAt(0).status;

                        if(g.estadoPeticion=="Aceptada")
                        {
                            existeAdopcionGato = aS.obtenerPor(a => a.idPeticionAdopcionFK==g.idPeticionAdopcion);

                            if(existeAdopcionGato.Count!=0)
                            {
                                g.idAdopcion = existeAdopcionGato.ElementAt(0).id;
                            }
                            else
                            {
                                g.idAdopcion = -1;
                            }
                        }
                        else
                        {
                            List<CitaAdopcion> citasGato = cS.obtenerPor(c => c.idPeticionAdopcionFK == g.idPeticionAdopcion);

                            if (citasGato.Count != 0)
                            {
                                g.idCitaPeticionAdopcion = citasGato.ElementAt(0).id;
                            }
                            else
                            {
                                g.idCitaPeticionAdopcion = -1;
                            }
                        }                        
                    }
                    else
                    {
                        g.idPeticionAdopcion = -1;
                        g.idCitaPeticionAdopcion = -1;
                    }

                    g.id = item.id;
                    g.idResponsableFK = item.idResponsableFK;
                    g.nombre = item.nombre;
                    g.edad = item.edad;
                    g.sexo = item.sexo;
                    g.foto = item.foto;
                    g.observaciones = item.observaciones;
                    g.padecimientos = item.padecimientos;
                    g.dieta = item.dieta;
                    g.status = item.status;

                    gatos.Add(g);
                }

                return View(gatos);
            }
            
            return HttpNotFound();                        
        }

        [Authorize(Roles="Adoptante")]
        // GET: Gato/Details/5
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUserServicio serv = new ApplicationUserServicio(db);
            var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
            ApplicationUser usuarioActual = usuario.ElementAt(0);

            ViewBag.idAdoptante = usuarioActual.Id;

            GatoServicio gS = new GatoServicio(db);
            Gato g= gS.obtenerPorID(new Gato { id = (int)id });

            if(g==null)
            {
                return HttpNotFound();
            }

            return View(g);
        }

        [Authorize(Roles = "Responsable, Veterinario, TrabajadorSocial")]
        public ActionResult Details2(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GatoServicio gs = new GatoServicio(db);
            Gato g = gs.obtenerPorID(new Gato { id = (int)id });

            if(User.IsInRole("Responsable"))
            {
                ViewBag.Rol = "Responsable";
            }
            else if(User.IsInRole("TrabajadorSocial"))
            {
                ViewBag.Rol = "TrabajadorSocial";                

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);

                ViewBag.IdTrabajadorSocial = usuarioActual.Id;
            }
            else
            {
                ViewBag.Rol = "Veterinario";

                ApplicationUserServicio serv = new ApplicationUserServicio(db);
                var usuario = serv.obtenerPor(u => u.UserName == User.Identity.Name);
                ApplicationUser usuarioActual = usuario.ElementAt(0);

                ViewBag.IdVeterinario = usuarioActual.Id;
            }

            if (g==null)
            {
                return HttpNotFound();
            }

            return View(g);
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
