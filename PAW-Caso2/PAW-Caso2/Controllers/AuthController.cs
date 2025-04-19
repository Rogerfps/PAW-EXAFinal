using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;
using PAW_Caso2.Services;

namespace PAW_Caso2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Registro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Id,NombreUsuario,NombreCompleto,Correo,Telefono,Contrasena,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool success = await _authService.Registro(usuario);

                if (!success)
                {
                    ViewBag.Message = "El nombre de usuario o el correo ya están en uso";
                    return View();
                }

                return RedirectToAction("Login");

            }
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string usuarioOrCorreo, string contrasena)
        {
            var usuario = await _authService.Autenticar(usuarioOrCorreo, contrasena);

            if (usuario == null)
            { 
                ViewBag.Message = "Usuario, correo o contraseña incorrectos";
                return View();
            }

            await _authService.ClaimCookie(HttpContext, usuario);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
