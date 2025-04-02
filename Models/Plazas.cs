using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ParqueoAutomotor.Models
{
    public class Plazas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoPlaza { get; set; } // 'Moto' o 'Vehiculo Ligero'
    }
}