using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ParqueoAutomotor.Data;
using ParqueoAutomotor.Models;
using ParqueoAutomotor.Services;

namespace ParqueoAutomotor.Controllers
{
    [System.Web.Http.RoutePrefix("api/vehiculos")]
    public class VehiculosController : ApiController
    {
        // GET: Vehiculos
        private readonly VehiculoService vehiculoService;
        public VehiculosController()
        {
            vehiculoService = new VehiculoService();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Listar")]
        public IHttpActionResult ListarVehiculos()
        {
            List<Vehiculos> vehiculos = vehiculoService.ObtenerVehiculos();
            return Ok(vehiculos);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Crear")]

        public IHttpActionResult CrearVehiculo([FromBody] Vehiculos vehiculo)
        {
            if (vehiculo == null)
                return BadRequest("Datos inválidos");

            bool resultado = vehiculoService.AgregarVehiculo(vehiculo);
            if (resultado)
                return Ok("Vehículo registrado exitosamente");
            else
                return BadRequest("Error al registrar el vehículo");
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("Actualizar/{id}")]
        public IHttpActionResult ActualizarVehiculo(int id, [FromBody] Vehiculos vehiculo)
        {
            if (vehiculo == null)
                return BadRequest("Datos inválidos");

            // Lógica para actualizar el vehículo en la base de datos
            bool resultado = vehiculoService.ActualizarVehiculo(id, vehiculo);

            if (resultado)
                return Ok("Vehículo actualizado correctamente");
            else
                return NotFound();
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("Eliminar/{id}")]
        public IHttpActionResult EliminarVehiculo(int id)
        {
            bool resultado = vehiculoService.EliminarVehiculo(id);

            if (resultado)
                return Ok("Vehículo eliminado correctamente");
            else
                return NotFound();
        }

    }
}