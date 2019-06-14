using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class Adopcion_TrabajadorSocialViewModel
    {
        [Key]
        public int idAdopcion { get; set; }
        public int idPeticionAdopcion { get; set; }
        public string idTrabajadorSocial { get; set; }
        public int idGato { get; set; }
        public string idAdoptante { get; set; }
        [Display(Name = "Nombre gato")]
        public string nombreGato { get; set; }
        [Display(Name = "Nombre adoptante")]
        public string nombreAdoptante { get; set; }
    }
}