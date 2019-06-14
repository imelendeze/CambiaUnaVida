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
    public class PeticionAdopcionServicio
    {
        private IUnitOfWork _iuow = null;
        private ApplicationDbContext context = null;
        private IGeneric<PeticionAdopcion> _iGen = null;

        public PeticionAdopcionServicio(ApplicationDbContext c)
        {
            context = c;
        }

        public IUnitOfWork iuow { get { return _iuow = _iuow ?? new UnitOfWork(context); } }
        public IGeneric<PeticionAdopcion> iGen { get { return _iGen = _iGen ?? new PeticionAdopcionRepository(context); } }


        public bool guardar(PeticionAdopcion ent)
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

        public List<PeticionAdopcion> obtenerPor(Expression<Func<PeticionAdopcion, bool>> condicion)
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

        public PeticionAdopcion obtenerPorID(PeticionAdopcion ent)
        {
            try
            {
                PeticionAdopcion entAux = iGen.obtenerPorId(ent);

                if (entAux != null)
                    return entAux;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void eliminar(PeticionAdopcion ent)
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

        public void actualizar(PeticionAdopcion ent)
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

        public List<PeticionAdopcion> obtenerTodo()
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