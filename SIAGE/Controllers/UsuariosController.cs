using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Services.Entities;
using Utils.Enums;
using Utils.Criptografia;
using Settings.Password;
using Settings.Email;
using Microsoft.AspNetCore.Authorization;
using Settings.Alert;

namespace SIAGE.Controllers
{
	[Authorize(Roles = "Gerente")]
	public class UsuariosController : Controller
	{
		private readonly UsuariosServices _context;

		public UsuariosController(UsuariosServices context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.GetUsuariosAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Usuario usuario)
		{
			try
			{
				var senha = new PasswordSettings().NovaSenha();
				usuario.Password = Criptografia.HashValue(senha);
				await _context.CreateUsuario(usuario);
				await EmailSenderAsync.SendEmailAsync(usuario.Email, senha);
				TempData["alert"] = new PutAlert().GetAlert("Parabéns", "Usuário criado com sucesso!", "success");
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["alert"] = new PutAlert().GetAlert("Erro", ex.Message, "error");
				return View(usuario);
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var user = await _context.GetUsuario(id);
				if (user == null)
					new Exception("Usuário não encontrado");

				return View(user);
			}
			catch (Exception ex)
			{
				TempData["alert"] = new PutAlert().GetAlert("Erro", ex.Message, "error");
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Usuario usuario, int id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _context.UpdateUsuario(usuario);
					TempData["alert"] = new PutAlert().GetAlert("Parabéns", "Usuário atualizado com sucesso!", "success");
					return RedirectToAction("Index");
				}
				return View(usuario);
			}
			catch (Exception ex)
			{
				TempData["alert"] = new PutAlert().GetAlert("Erro", ex.Message, "error");
				return View(usuario);
			}
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var user = await _context.GetUsuario(id);
				if (user == null)
					new Exception("Usuário não encontrado");

				return PartialView("_deleteUsuario", user);
			}
			catch (Exception ex)
			{
				TempData["alert"] = new PutAlert().GetAlert("Erro", ex.Message, "error");
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Usuario usuario, int id)
		{
			try
			{
				usuario = await _context.GetUsuario(id);
				await _context.ExcluirUsuario(usuario);
				TempData["alert"] = new PutAlert().GetAlert("Parabéns", "Usuário excluido com sucesso!", "success");
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["alert"] = new PutAlert().GetAlert("Erro", ex.Message, "error");
				return RedirectToAction("Index");
			}
		}
	}
}