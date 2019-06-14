using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class VisitaTrabajadorSocial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string idTrabajadorSocialFK { get; set; }        
        public string idAdoptanteFK { get; set; }
        [ForeignKey("GatoFK")]
        public int idGatoFK { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [StringLength(10)]
        public string hora { get; set; }
        
        public virtual Gato GatoFK { get; set; }        
    }
}