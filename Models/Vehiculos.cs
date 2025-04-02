using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueoAutomotor.Models
{
    public class Vehiculos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Placa { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoVehiculo { get; set; } // 'Moto' o 'Vehiculo Ligero'

        [Required]
        public int EsHibridoOElectrico { get; set; } // true = Sí, false = No

        [Required]
        public DateTime HoraIngreso { get; set; }

        public DateTime? HoraSalida { get; set; }

        [Required]
        public int PlazaAsignada { get; set; }

    }
}