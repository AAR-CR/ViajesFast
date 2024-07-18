
using System.Net;
using ViajesFast.Models;

namespace ViajesFast.Services
{
    public class VueloService
    {
        private readonly HttpClient _httpClient;

        public VueloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Vuelo>> GetVuelosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Vuelo>>("api/vuelos");
        }

        public async Task<Vuelo> GetVueloByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Vuelo>($"api/vuelos/{id}");
        }


        public async Task<Vuelo> CreateVueloAsync(Vuelo vuelo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vuelos", vuelo);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al crear el vuelo: {response.StatusCode}, Detalles: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<Vuelo>();
        }
    }

    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("api/usuarios");
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/usuarios/{id}");
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios", usuario);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al crear el usuario: {response.StatusCode}, Detalles: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<Usuario> GetUsuarioByCorreoAsync(string correo)
        {
            var response = await _httpClient.GetAsync($"api/usuarios/buscarPorCorreo?correo={correo}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al buscar el usuario: {response.StatusCode}", null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/usuarios/{usuario.Id}", usuario);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al actualizar el usuario: {response.StatusCode}, Detalles: {errorContent}");
            }
            // Manejar posibles respuestas vacías
            if (response.Content.Headers.ContentLength == 0)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al borrar el usuario: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }

    public class ReservaService
    {
        private readonly HttpClient _httpClient;

        public ReservaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reserva>> GetReservasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Reserva>>("api/reservas");
        }

        public async Task<Reserva> GetReservaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Reserva>($"api/reservas/{id}");
        }

        public async Task<Reserva> CreateReservaAsync(Reserva reserva)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reservas", reserva);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al crear la reserva: {response.StatusCode}, Detalles: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<Reserva>();
        }

        public async Task<List<Reserva>> GetReservasByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/reservas/usuario/{usuarioId}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<Reserva>(); 
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Reserva>>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Error al obtener las reservas: {ex.StatusCode}, Detalles: {ex.Message}");
            }
        }
    }

    

}
