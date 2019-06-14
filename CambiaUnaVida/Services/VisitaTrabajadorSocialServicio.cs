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
    public class VisitaTrabajadorSocialServicio
    {
        private IUnitOfWork _iuow = null;
        private ApplicationDbContext context = null;
        private IGeneric<VisitaTrabajadorSocial> _iGen = null;

        public VisitaTrabajadorSocialServicio(ApplicationDbContext c)
        {
            context = c;
        }

        public IUnitOfWork iuow { get { return _iuow = _iuow ?? new UnitOfWork(context); } }
        public IGeneric<VisitaTrabajadorSocial> iGen { get { return _iGen = _iGen ?? new VisitaTrabajadorSocialRepository(context); } }

        public bool guardar(VisitaTrabajadorSocial ent)
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

        public List<VisitaTrabajadorSocial> obtenerPor(Expression<Func<VisitaTrabajadorSocial, bool>> condicion)
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

        public VisitaTrabajadorSocial obtenerPorID(VisitaTrabajadorSocial ent)
        {
            try
            {
                VisitaTrabajadorSocial entAux = iGen.obtenerPorId(ent);

                if (entAux != null)
                    return entAux;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void eliminar(VisitaTrabajadorSocial ent)
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

        public void actualizar(VisitaTrabajadorSocial ent)
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

        public List<VisitaTrabajadorSocial> obtenerTodo()
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