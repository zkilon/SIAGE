using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "O Email é obrigatório")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "A Senha é obrigatória")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string Password { get; set; }
	}
}
