using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Dominio
{
    public class ReporteCitaVeterinario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("CitaVeterinarioFK")]
        public int idCitaVeterinarioFK { get; set; }
        [Required]
        [StringLength(400)]
        public string observaciones { get; set; }

        public virtual CitaVeterinario CitaVeterinarioFK { get; set; }
    }
}