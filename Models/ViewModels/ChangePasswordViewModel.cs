using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
	public class ChangePasswordViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "A Senha é obrigatória")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string Password { get; set; }

		[Required(ErrorMessage = "A Senha é obrigatória")]
		[DataType(DataType.Password)]
		[Display(Name = "Nova Senha")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "A Senha é obrigatória")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirme a nova senha")]
		[Compare(nameof(NewPassword), ErrorMessage = "As senhas devem ser iguais")]
		public string ConfirmPassword { get; set; }
	}
}
