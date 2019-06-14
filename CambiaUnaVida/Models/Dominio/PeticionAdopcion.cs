using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class PeticionAdopcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Propiedad autoincrementable
        public int id { get; set; }
        [ForeignKey("AdoptanteFK")]
        public string idAdoptanteFK { get; set; }   
        [ForeignKey("GatoFK")]
        public int idGatoFK { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [StringLength(15)]
        public string status { get; set; }
        [StringLength(250)]
        public string observaciones { get; set; }

        //Navegacion
        public virtual ApplicationUser AdoptanteFK { get; set; }
        public virtual Gato GatoFK { get; set; }                
    }
}