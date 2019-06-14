using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class Responsable_GatoPeticionCitaAdopcionViewModel
    {
        [Key]        
        public int id { get; set; }        
        public string idResponsableFK { get; set; }
        [Display(Name= "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Edad")]
        public string edad { get; set; }
        [Display(Name = "Sexo")]
        public string sexo { get; set; }
        [Display(Name = "Foto")]
        public string foto { get; set; }
        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }
        [Display(Name = "Padecimientos")]
        public string padecimientos { get; set; }
        [Display(Name = "Dieta")]
        public string dieta { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        public int idPeticionAdopcion { get; set; }
        public int idCitaPeticionAdopcion { get; set; }
        public string estadoPeticion { get; set; }
        public int idAdopcion { get; set; }
    }
}