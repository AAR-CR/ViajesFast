using API_Vuelos.Models;
using API_Vuelos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Vuelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            return _usuarioService.GetUsuarios();
        }

        //[HttpGet]
        //public ActionResult<List<Reserva>> GetReservasByUsuarioId(int id)
        //{
        //    var usuario = _usuarioService.GetUsuarioById(id);
        //    List<Reserva> result = (List<Reserva>)usuario.Reservas;
        //    return result;
        //}


        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuarioById(int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public ActionResult<Usuario> AddUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedUsuario = _usuarioService.AddUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = addedUsuario.Id }, addedUsuario);
        }

        [HttpGet("buscarPorCorreo")]
        public ActionResult<Usuario> GetUsuarioPorCorreo(string correo)
        {
            var usuario = _usuarioService.GetUsuarioByCorreo(correo);
            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                _usuarioService.UpdateUsuario(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_usuarioService.GetUsuarioById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var success = _usuarioService.DeleteUsuario(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }



    }
}
