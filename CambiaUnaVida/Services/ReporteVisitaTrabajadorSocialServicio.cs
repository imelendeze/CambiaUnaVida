using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambiaUnaVida.Models.Repository;
using CambiaUnaVida.Models.Dominio;
using CambiaUnaVida.Models.Repository.Infrastructure;
using CambiaUnaVida.Models.Repository.EntityRepository;
using System.Linq.Expressions;
using CambiaUnaVida.Models;

namespace CambiaUnaVida.Services
{
    public class ReporteVisitaTrabajadorSocialServicio
    {
        private IUnitOfWork _iuow = null;
        private ApplicationDbContext context = null;
        private IGeneric<ReporteVisitaTrabajadorSocial> _iGen = null;

        public ReporteVisitaTrabajadorSocialServicio(ApplicationDbContext c)
        {
            context = c;
        }

        public IUnitOfWork iuow { get { return _iuow = _iuow ?? new UnitOfWork(context); } }
        public IGeneric<ReporteVisitaTrabajadorSocial> iGen { get { return _iGen = _iGen ?? new ReporteVisitaTrabajadorSocialRepository(context); } }

        public bool guardar(ReporteVisitaTrabajadorSocial ent)
        {
            try
            {
                iGen.crear(ent);
                iuow.commit();

                return true;
            }
            catch (Exception e)
            {
                iuow.rollback();
                return false;
            }
        }

        public List<ReporteVisitaTrabajadorSocial> obtenerPor(Expression<Func<ReporteVisitaTrabajadorSocial, bool>> condicion)
        {
            try
            {
                return iGen.encontrarPor(condicion).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ReporteVisitaTrabajadorSocial obtenerPorID(ReporteVisitaTrabajadorSocial ent)
        {
            try
            {
                ReporteVisitaTrabajadorSocial entAux = iGen.obtenerPorId(ent);

                if (entAux != null)
                    return entAux;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void eliminar(ReporteVisitaTrabajadorSocial ent)
        {
            try
            {
                iGen.eliminar(ent);
                iuow.commit();
            }
            catch (Exception e)
            {
                iuow.rollback();
            }
        }

        public void actualizar(ReporteVisitaTrabajadorSocial ent)
        {
            try
            {
                iGen.actualizar(ent);
                iuow.commit();
            }
            catch (Exception e)
            {
                iuow.rollback();
            }
        }

        public List<ReporteVisitaTrabajadorSocial> obtenerTodo()
        {
            try
            {
                return iGen.obtenerTodo().ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}