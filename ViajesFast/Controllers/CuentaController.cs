using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ViajesFast.Models;
using ViajesFast.Data;
using Microsoft.AspNetCore.Authorization;
using ViajesFast.Encript;
using Microsoft.EntityFrameworkCore;
using ViajesFast.Services;

namespace ViajesFast.Controllers
{
                                                           //SECCION DE MANEJO DE CUENTAS
    public class CuentaController : Controller
    {
        private readonly ViajesFastDbConext _context;
        private readonly UsuarioService _usuarioService;
        private readonly ReservaService _reservaService;
        private readonly VueloService _VueloService;

        public CuentaController(ViajesFastDbConext context, UsuarioService usuarioService, ReservaService reservaService, VueloService vueloService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _reservaService = reservaService;   
            _VueloService = vueloService;

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario usuario)
        {
          
            // Se encripta la contraseña antes de registrrla: 
            var contra = Encriptar.GetSHA256(usuario.Contraseña);
            usuario.Contraseña = contra;

            // Validar el modelo
            if (ModelState.IsValid)
            {
                //var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == usuario.CorreoElectronico);

                Usuario usuarioApiExiste=null;

                //var usuarioApiExiste = await _usuarioService.GetUsuarioByCorreoAsync(usuario.CorreoElectronico);
                try
                {
                    usuarioApiExiste = await _usuarioService.GetUsuarioByCorreoAsync(usuario.CorreoElectronico);
                }
                catch (HttpRequestException ex)
                {
                    // Si el status code es 404, el usuario no existe, podemos proceder
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        usuarioApiExiste = null;
                    }
                    else
                    {
                        // Si es otro tipo de error, manejarlo adecuadamente
                        ViewBag.ErrorMessage = "Error al verificar el usuario existente: " + ex.Message;
                        return View("Error");
                    }
                }
                if (usuarioApiExiste != null)
                {
                    ModelState.AddModelError("CorreoElectronico", "Ya existe un usuario con este correo registrado.");
                    return View(usuario);
                }
                try
                {
                    //_context.Usuarios.Add(usuario);
                    //await _context.SaveChangesAsync();
                    await _usuarioService.CreateUsuarioAsync(usuario);

                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    ViewBag.ErrorMessage = "Ocurrió un error al registrar el usuario. Por favor, inténtelo de nuevo.";
                    ViewBag.ErrorDetails = ex.Message;
                    return View("Error");
                }
            }
            TempData["SuccessMessage"] = "Usuario creado exitosamente. Ingresa acá:";
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string correoElectronico, string contraseña)
        {
            if (ModelState.IsValid)
            {
                string hash = Encript.Encriptar.GetSHA256(contraseña);

                var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
                //var usuarioApi = _usuarioService.GetUsuarioByCorreoAsync(correoElectronico);

                if (usuario != null && usuario.Contraseña == hash)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.CorreoElectronico),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Correo electrónico o contraseña incorrectos.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }




        public async Task<IActionResult> Profile()
        {
            var correoElectronico = User.Identity.Name;
            Usuario usuario = null;

            try
            {
                usuario = await _usuarioService.GetUsuarioByCorreoAsync(correoElectronico);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener el usuario: {ex.Message}";
                return View("Error");
            }

            if (usuario != null)
            {
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(Usuario usuario)
        {
            usuario.Contraseña = Encriptar.GetSHA256(usuario.Contraseña);

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedUsuario = await _usuarioService.UpdateUsuarioAsync(usuario);
                    
                    TempData["SuccessMessage"] = "Perfil actualizado exitosamente.";
                }
                catch (HttpRequestException ex)
                {

                    ViewBag.ErrorMessage = $"Error al actualizar el usuario: {ex.Message}";
                    return View("Error");
                }

                return RedirectToAction("Profile");
            }
            return View(usuario);
        }


        public async Task<IActionResult> HistorialReservas()
        {
            var correoElectronico = User.Identity.Name;
            Usuario usuario = null;

            try
            {
                usuario = await _usuarioService.GetUsuarioByCorreoAsync(correoElectronico);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                var reservas = await _reservaService.GetReservasByUsuarioIdAsync(usuario.Id);
                if (reservas == null || reservas.Count == 0)
                {
                    ViewBag.Message = "No tienes reservas.";
                    return View(new List<Reserva>());
                }

                return View(reservas);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener el historial de reservas: {ex.Message}";
                return View("Error");
            }
        }


    }
}
