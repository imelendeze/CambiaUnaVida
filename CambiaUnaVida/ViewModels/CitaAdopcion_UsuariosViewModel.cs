using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class CitaAdopcion_UsuariosViewModel
    {
        [Key]
        public int idCitaAdopcion { get; set; }
        public int idPeticionAdopcion { get; set; }        
        public string idTrabajadorSocial { get; set; }
        [Display(Name="Trabajador social asignado:")]
        public string nombreTrabajadorSocial { get; set; }
        [Display(Name = "Fecha cita:")]
        public DateTime fecha { get; set; }
        [Display(Name = "Hora cita:")]
        public string hora { get; set; }        
    }
}