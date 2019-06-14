using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class CitaAdopcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Propiedad autoincrementable
        public int id { get; set; }        
        [ForeignKey("PeticionAdopcionFK")]
        public int idPeticionAdopcionFK { get; set; }    
        [ForeignKey("TrabajadorSocialFK")]
        public string idTrabajadorSocialFK { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [StringLength(10)]
        public string hora { get; set; }
        
        public virtual PeticionAdopcion PeticionAdopcionFK { get; set; }
        public virtual ApplicationUser TrabajadorSocialFK { get; set; }
    }
}