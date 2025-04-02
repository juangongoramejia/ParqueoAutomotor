using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueoAutomotor.Models
{
    public class Transacciones
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehiculoId { get; set; }  

        [Required]
        public int Monto { get; set; }

        [Required]
        public DateTime FechaTransaccion { get; set; }
    }
}