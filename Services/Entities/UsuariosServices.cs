using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Context;

namespace Services.Entities
{
	public class UsuariosServices
	{
		private readonly SIAGEContext _context;

		public UsuariosServices(SIAGEContext context)
		{
			_context = context;
		}

		public async Task<List<Usuario>> GetUsuariosAsync()
		{
			try
			{
				return await _context.Usuarios.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Usuario> GetUsuario(int id)
		{
			try
			{
				return await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Usuario> GetUsuario(string email, string password)
		{
			try
			{
				return await _context.Usuarios.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task CreateUsuario(Usuario usuario)
		{
			try
			{
				_context.Usuarios.Add(usuario);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task UpdateUsuario(Usuario usuario)
		{
			try
			{
				_context.Usuarios.Update(usuario);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task ExcluirUsuario(Usuario usuario)
		{
			try
			{
				_context.Usuarios.Remove(usuario);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<string> GetSenhaUsuario(int id)
		{
			try
			{
				return await _context.Usuarios.Where(x => x.Id == id).Select(x => x.Password).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task SetLogin(Usuario usuario)
		{
			try
			{
				_context.Usuarios.Update(usuario);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
