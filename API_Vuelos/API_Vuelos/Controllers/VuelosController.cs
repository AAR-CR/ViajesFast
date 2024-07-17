using API_Vuelos.Models;
using API_Vuelos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Vuelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VuelosController : ControllerBase
    {
        private readonly VueloService _vueloService;

        public VuelosController(VueloService vueloService)
        {
            _vueloService = vueloService;
        }

        [HttpGet]
        public ActionResult<List<Vuelo>> GetVuelos()
        {
            return _vueloService.GetVuelos();
        }

        [HttpGet("{id}")]
        public ActionResult<Vuelo> GetVueloById(int id)
        {
            var vuelo = _vueloService.GetVueloById(id);

            if (vuelo == null)
            {
                return NotFound();
            }

            return vuelo;
        }

        [HttpPost]
        public ActionResult<Vuelo> AddVuelo(Vuelo vuelo)
        {
            var addedVuelo = _vueloService.AddVuelo(vuelo);
            return CreatedAtAction(nameof(GetVueloById), new { id = addedVuelo.Id }, addedVuelo);
        }
    }
}
