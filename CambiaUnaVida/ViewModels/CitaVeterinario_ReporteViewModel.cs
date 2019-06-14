using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.ViewModels
{
    public class CitaVeterinario_ReporteViewModel
    {
        [Key]        
        public int id { get; set; }
        public string idAdoptanteFK { get; set; }
        public string idVeterinarioFK { get; set; }        
        public int idGato { get; set; }        
        public DateTime fecha { get; set; }        
        public string hora { get; set; }        
        public string status { get; set; }
        public int idReporteCita { get; set; }
    }
}