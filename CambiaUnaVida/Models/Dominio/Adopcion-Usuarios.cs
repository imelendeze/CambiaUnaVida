using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class Adopcion_Usuarios
    {
        [Key]
        [ForeignKey("idAdopcion")]       
        [Column(Order = 1)]
        public int idAdopcionFK { get; set; }
        [Key]
        [ForeignKey("idUsu")]
        [Column(Order = 2)]
        public string idUsuFK { get; set; }
        
        public virtual ApplicationUser idUsu { get; set; }        
        public virtual Adopcion idAdopcion { get; set; }
    }
}