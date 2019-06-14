using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class ReporteVisitaTrabajadorSocialRepository : IGeneric<ReporteVisitaTrabajadorSocial>
    {
        ApplicationDbContext db;

        public ReporteVisitaTrabajadorSocialRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(ReporteVisitaTrabajadorSocial Tentity)
        {
            ReporteVisitaTrabajadorSocial TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<ReporteVisitaTrabajadorSocial>(TentityAux).State = EntityState.Detached;
                db.Entry<ReporteVisitaTrabajadorSocial>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(ReporteVisitaTrabajadorSocial Tentity)
        {
            db.ReportesVisitasTrabajadorSocial.Add(Tentity);
        }

        public void eliminar(ReporteVisitaTrabajadorSocial Tentity)
        {
            db.Entry<ReporteVisitaTrabajadorSocial>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<ReporteVisitaTrabajadorSocial> encontrarPor(Expression<Func<ReporteVisitaTrabajadorSocial, bool>> condicion)
        {
            return db.ReportesVisitasTrabajadorSocial.Where(condicion).Select(c => c);
        }

        public ReporteVisitaTrabajadorSocial obtenerPorId(ReporteVisitaTrabajadorSocial Tentity)
        {
            return db.ReportesVisitasTrabajadorSocial.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<ReporteVisitaTrabajadorSocial> obtenerTodo()
        {
            return db.ReportesVisitasTrabajadorSocial.Select(c => c);
        }
    }
}