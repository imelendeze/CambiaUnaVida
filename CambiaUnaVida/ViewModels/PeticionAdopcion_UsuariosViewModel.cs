using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class PeticionAdopcion_UsuariosViewModel
    {
        [Key]        
        public int id { get; set; }  
        public int idCitaAdopcion { get; set; }
        public int idGatoFK { get; set; }
        [Display(Name= "Nombre del gato:")]
        public string nombreGato { get; set; }
        public DateTime fecha { get; set; }        
        public string status { get; set; }        
        public string observaciones { get; set; }
    }
}