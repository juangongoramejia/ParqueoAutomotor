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
    [System.Web.Http.RoutePrefix("api/transacciones")]
    public class TransaccionesController : ApiController
    {
        private TransaccionService transaccionService;

        public TransaccionesController()
        {
            transaccionService = new TransaccionService();
        }

        // Método GET para listar las transacciones
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Listar")]
        public IHttpActionResult ListarTransacciones()
        {
            var transacciones = transaccionService.ListarTransacciones();

            if (transacciones.Count == 0)
                return NotFound(); // Si no hay transacciones, devuelve Not Found

            return Ok(transacciones); // Retorna las transacciones en formato JSON
        }

        // Método GET para calcular las ganancias del día
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("CalcularGananciasDelDia")]
        public IHttpActionResult CalcularGananciasDelDia()
        {
            int total = transaccionService.CalcularGananciasDelDia();
            return Ok(new { Ganancias = total }); // Retorna las ganancias del día en formato JSON
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult SalirVehiculo(int vehiculoId)
        {
            // Crear una instancia de VehiculoService
            var vehiculoService = new VehiculoService();

            // Obtener el vehículo usando el ID
            var vehiculo = vehiculoService.ObtenerVehiculoPorId(vehiculoId);

            if (vehiculo == null)
                return NotFound();  // Si no se encuentra el vehículo, retorna 404

            // Calcular el monto usando el método CalcularMonto en VehiculoService
            int monto = vehiculoService.CalcularMonto(vehiculo);

            // Generar la transacción
            bool transaccionCreada = transaccionService.GenerarTransaccion(vehiculoId, monto);

            if (transaccionCreada)
                return Ok("Transacción registrada exitosamente");
            else
                return BadRequest("Error al generar la transacción");
        }

    }
}