using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CambiaUnaVida.Models.Dominio
{
    public class Gato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Propiedad autoincrementable
        public int id { get; set; }  
        [ForeignKey("ResponsableFK")]
        [Required]        
        public string idResponsableFK { get; set; }
        [Required]
        [StringLength(20)]
        public string nombre { get; set; }
        [Required]
        [StringLength(20)]
        public string edad { get; set; }
        [Required]
        [StringLength(15)]
        public string sexo { get; set; }
        [Required]
        [StringLength(80)]
        public string foto { get; set; }
        [Required]
        [StringLength(250)]
        public string observaciones { get; set; }
        [Required]
        [StringLength(250)]
        public string padecimientos { get; set; }
        [Required]
        [StringLength(250)]
        public string dieta { get; set; }
        [Required]
        [StringLength(15)]
        public string status { get; set; }        

        public virtual ApplicationUser ResponsableFK { get; set; }        
    }
}