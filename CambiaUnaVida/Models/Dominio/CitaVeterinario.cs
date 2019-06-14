using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class CitaVeterinario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }        
        public string idAdoptanteFK { get; set; }        
        public string idVeterinarioFK { get; set; }    
        [ForeignKey("GatoFK")]
        public int idGato { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [StringLength(10)]
        public string hora { get; set; }
        [Required]
        [StringLength(15)]
        public string status { get; set; }
                
        public virtual Gato GatoFK { get; set; }        
    }
}