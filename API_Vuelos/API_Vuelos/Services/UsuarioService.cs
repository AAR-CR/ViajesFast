using API_Vuelos.Data;
using API_Vuelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Vuelos.Services
{
    public class UsuarioService
    {
        private readonly ViajesFastDbConext _context;

        public UsuarioService(ViajesFastDbConext context)
        {
            _context = context;
        }

        public List<Usuario> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario AddUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario GetUsuarioByCorreo(string correo)
        {
            return _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correo);
        }

        public Usuario UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public bool DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
    }
}
