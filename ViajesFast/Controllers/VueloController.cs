using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.Security.Claims;
using ViajesFast.Data;
using ViajesFast.Extensions;
using ViajesFast.Models;
using ViajesFast.Services;


namespace ViajesFast.Controllers
{
    public class VueloController : Controller
    {
        private static List<Vuelo> _carrito = new List<Vuelo>();

        private readonly VueloService _vueloService;
        private readonly UsuarioService _usuarioService;
        private readonly ReservaService _reservaService;
        private readonly ILogger _logger;
        private readonly EmailService _emailService;

        public VueloController(VueloService vueloService, UsuarioService usuarioService, ReservaService reservaService, ILogger<VueloController> logger, EmailService emailService)
        {
            _vueloService = vueloService;
            _usuarioService = usuarioService;
            _reservaService = reservaService;
            _logger = logger;
            _emailService = emailService;
        }


        [Authorize]
        public IActionResult Cart(Vuelo vuelo)
        {
            if (vuelo != null && !String.IsNullOrEmpty(vuelo.Destino))
            {
                _carrito.Add(vuelo);
                return View(_carrito);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.GetUserId();
            var id = int.Parse(userId);
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }
            
            foreach (var vuelo in _carrito)
            {
                var crearVuelo = await _vueloService.CreateVueloAsync(vuelo);
                var reserva = new Reserva
                {
                    UsuarioId = usuario.Id,
                    VueloId = crearVuelo.Id,
                    FechaReserva = DateTime.UtcNow
                };

                await _reservaService.CreateReservaAsync(reserva);
                string subject = "Confirmación de Reserva";
                string message = "<h1>Reserva Confirmada</h1><p>Gracias por reservar con nosotros.</p>";
                await _emailService.SendEmailAsync(usuario.CorreoElectronico, subject, message);

            }

            // Vaciar el carrito después de la compra
            _carrito.Clear();

            return View();
        }



        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }




    }
}
