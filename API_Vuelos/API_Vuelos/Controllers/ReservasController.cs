using API_Vuelos.Models;
using API_Vuelos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Vuelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public ActionResult<List<Reserva>> GetReservas()
        {
            return _reservaService.GetReservas();
        }

        [HttpGet("{id}")]
        public ActionResult<Reserva> GetReservaById(int id)
        {
            var reserva = _reservaService.GetReservaById(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        [HttpPost]
        public ActionResult<Reserva> AddReserva(Reserva reserva)
        {
            var addedReserva = _reservaService.AddReserva(reserva);

            if (addedReserva == null)
            {
                return BadRequest("Usuario o Vuelo no encontrado.");
            }

            return CreatedAtAction(nameof(GetReservaById), new { id = addedReserva.Id }, addedReserva);
        }
    }
}
