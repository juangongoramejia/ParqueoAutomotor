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
    [System.Web.Http.RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        private readonly UsuarioService usuarioService;

        public UsuarioController()
        {
            usuarioService = new UsuarioService();
        }

        // POST: api/Usuario/verificar
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("verificar")]
        public IHttpActionResult VerificarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Clave))
                return BadRequest("Nombre de usuario y contraseña son requeridos.");

            var usuarioExistente = usuarioService.ObtenerPorNombre(usuario.Nombre);

            if (usuarioExistente != null)
            {
                string hashedInput = usuarioService.HashPassword(usuario.Clave);

                if (usuarioExistente.Clave == hashedInput)
                    return Ok("Inicio de sesión exitoso");
                else
                    return BadRequest("Contraseña incorrecta");
            }

            return NotFound(); // Usuario no encontrado
        }

        // POST: api/Usuario/crear
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("crear")]
        public IHttpActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Clave))
                return BadRequest("Nombre de usuario y contraseña son requeridos.");

            var usuarioExistente = usuarioService.ObtenerPorNombre(usuario.Nombre);

            if (usuarioExistente != null)
                return BadRequest("El usuario ya existe.");

            bool creado = usuarioService.CrearUsuario(usuario);

            if (creado)
                return Ok("Usuario creado exitosamente");
            else
                return InternalServerError();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("VerificarExistencia/{Nombre}")]
        public IHttpActionResult VerificarExistencia(string Nombre)
        {
            try
            {
                bool existe = usuarioService.ObtenerPorNombre(Nombre) != null;
                return Ok(existe);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




    }
}