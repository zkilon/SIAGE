using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.ViewModels;
using Services.Entities;
using Utils.Criptografia;
using Utils.Enums;

namespace SIAGE.Controllers
{
	public class AutenticacaoController : Controller
	{
		private readonly UsuariosServices _context;

		public AutenticacaoController(UsuariosServices context)
		{
			_context = context;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel usuario)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var senha = Criptografia.HashValue(usuario.Password);
					var user = await _context.GetUsuario(usuario.Email, senha);

					if (user == null)
						throw new Exception("Usuário não encontrado");

					await Entrar(user);
					await SetLogin(user);
					return RedirectToAction("Index", "Home");
				}
				return View(usuario);
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
				return View(usuario);
			}
		}

		private async Task Entrar(Usuario usuario)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
				new Claim(ClaimTypes.Name, usuario.Name),
				new Claim(ClaimTypes.GivenName, usuario.Nickname),
				new Claim(ClaimTypes.Email, usuario.Email),
				new Claim(ClaimTypes.Role, Enum.Parse(typeof(FunctionEnum), usuario.FunctionId.ToString()).ToString()),
				new Claim(ClaimTypes.DateOfBirth, usuario.BirthDate.ToString()),
				new Claim(ClaimTypes.UserData, usuario.LastLogin.ToString())
			};

			var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
			ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

			var propriedadesDeAutenticacao = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(8),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
		}

		private async Task SetLogin(Usuario usuario)
		{
			try
			{
				usuario.LastLogin = DateTime.Now;
				await _context.SetLogin(usuario);
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
			}
		}

		public async Task<IActionResult> Sair()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}

		public async Task<IActionResult> ChangePassword(int id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var user = await _context.GetUsuario(id);

					if (user == null)
						throw new Exception("Usuário não encontrado");


					return PartialView("_changePassword");
				}
				return View();
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
				return View();
			}
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel usuario)
		{
			try
			{
				var link = HttpContext.Request.Headers["Referer"].ToString();
				if (ModelState.IsValid)
				{
					var user = await _context.GetUsuario(usuario.Id);
					if (Criptografia.HashValue(usuario.Password) == user.Password)
					{
						var senha = Criptografia.HashValue(usuario.NewPassword);
						user.Password = senha;
						await _context.UpdateUsuario(user);
					}
					else
					{
						ViewData["Error"] = "A senha não atual não está correta";
					}
					
				}
				ViewData["Error"] = "Senha alterada com sucesso";
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
				return View(usuario);
			}
		}
	}
}