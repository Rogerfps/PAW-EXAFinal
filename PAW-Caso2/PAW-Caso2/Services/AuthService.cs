using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;


namespace PAW_Caso2.Services
{
    public class AuthService
    {
        private readonly EventCorpContext _context;

        public AuthService(EventCorpContext context)
        {
            _context = context;
        }

        //public async Task<bool> Registro(string usuario, string nombre, string email, string telefono, string contrasena, string rol)
        //{
        //    // Validar si un usuario con el mismo correo o nombre de usuario ya existe
        //    if (_context.Usuarios.Any(u => u.Correo == email))
        //        return false;

        //    if (_context.Usuarios.Any(u => u.NombreUsuario == usuario))
        //        return false;

        //    // Crear un nuevo usuario
        //    var nuevoUsuario = new Usuario
        //    {
        //        NombreUsuario = usuario,
        //        NombreCompleto = nombre,
        //        Correo = email,
        //        Telefono = telefono,
        //        Contrasena = contrasena,
        //        Rol = rol
        //    };

        //    _context.Usuarios.Add(nuevoUsuario);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> Registro(Usuario usuario)
        {
            // Validar si un usuario con el mismo correo o nombre de usuario ya existe
            if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
                return false;

            if (_context.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                return false;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> Autenticar(string usuarioOrCorreo, string contrasena)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Correo == usuarioOrCorreo && u.Contrasena == contrasena || u.NombreUsuario == usuarioOrCorreo && u.Contrasena == contrasena)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    NombreCompleto = u.NombreCompleto,
                    Correo = u.Correo,
                    Telefono = u.Telefono,
                    Rol = u.Rol
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return null;

            return usuario;
        }

        public async Task ClaimCookie(HttpContext httpContext, Usuario usuario)
        { 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        public async Task<Usuario> GetUsuario(HttpContext httpContext)
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
                return null;
            var usuarioId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            return usuario;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
