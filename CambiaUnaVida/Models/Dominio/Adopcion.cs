using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Dominio
{
    public class Adopcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Propiedad autoincrementable
        public int id { get; set; }    
        [ForeignKey("PeticionAdopcionFK")]
        public int idPeticionAdopcionFK { get; set; }
        public string idTrabajadorSocialFK { get; set; }        
        public string idVeterinarioFK { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [StringLength(10)]
        public string hora { get; set; }
                
        public virtual PeticionAdopcion PeticionAdopcionFK { get; set; }        
    }
}