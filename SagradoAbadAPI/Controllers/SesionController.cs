using System.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SagradoAbadAPI.Modelos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System;
using SagradoAbadAPI.Contexto;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using SagradoAbadAPI.DTOs.Usuarios;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController(IConfiguration configuration, ContextoDb context) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDTO>> Register([FromBody] RegistrarUsuarioDTO usuario)
        {
            if (await context.Usuarios.AnyAsync(u => u.CorreoElectronico == usuario.CorreoElectronico))
            {
                return BadRequest("Usuario ya existente");
            }

            var hashedPassword = new PasswordHasher<RegistrarUsuarioDTO>().HashPassword(usuario, usuario.Password);
            usuario.Password = hashedPassword;

            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = usuario.Nombre,
             
                CorreoElectronico = usuario.CorreoElectronico,
                Password = hashedPassword,
              
            };

            context.Usuarios.Add(nuevoUsuario);
            await context.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] IniciarSesionDTO usuario)
        {
            var user = await context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == usuario.CorreoElectronico);
            if (user is null)
            {
                return BadRequest("Invalid username or password.");
            }
            if (new PasswordHasher<Usuario>().VerifyHashedPassword(user, user.Password, usuario.Password)
                 == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok(CreateToken(user));
        }


        private string CreateToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Rol.ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
