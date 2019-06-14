using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class Gato_ResponsableViewModel
    {
        [Key]
        public int idGato { get; set; }
        public string idResponsable { get; set; }
        [Display(Name = "Nombre Responsable")]
        public string nombreResponsable { get; set; }
        [Display(Name = "Nombre Gato")]
        public string nombreGato { get; set; }
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
    }
}