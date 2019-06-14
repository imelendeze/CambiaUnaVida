using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class Adopcion_Usuarios_GatoViewModel
    {
        [Key]        
        public int idAdopcion { get; set; }        
        public int idPeticionAdopcionFK { get; set; }
        [Display(Name= "Nombre del trabajador social")]
        public string nombreTrabajador { get; set; }
        public string idTrabajadorSocialFK { get; set; }
        [Display(Name = "Nombre del veterinario")]
        public string nombreVeterinario { get; set; }
        public string idVeterinarioFK { get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Display(Name = "Nombre del gato")]
        public string nombreGato { get; set; }
        public int idGatoFK { get; set; }
        public string idAdoptante { get; set; }
    }
}