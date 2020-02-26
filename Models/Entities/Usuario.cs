using System;
using System.ComponentModel.DataAnnotations;
using Utils.Enums;
using Utils.Exceptions;

namespace Models.Entities
{
	public class Usuario : EntitiBase
	{
		[Required(ErrorMessage = "O Nome é obrigatório")]
		[Display(Name = "Nome")]
		public string Name { get; set; }

		[Required(ErrorMessage = "O Apelido é obrigatório")]
		[Display(Name = "Apelido")]
		public string Nickname { get; set; }

		[Required(ErrorMessage = "O Email é obrigatório")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "A Senha é obrigatória")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string Password { get; set; }

		[Display(Name = "Função")]
		public FunctionEnum Function { get; set; }

		[Required(ErrorMessage = "A Função é obrigatória")]
		[Display(Name = "Função")]
		public int FunctionId { get; set; }

		[Required(ErrorMessage = "A Data de nascimento é obrigatória")]
		[DataType(DataType.Date)]
		[Display(Name = "Data de Nascimento")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Ativo")]
		public bool Active { get; set; } = true;

		[DataType(DataType.Date)]
		[Display(Name = "Último Login")]
		public DateTime LastLogin { get; set; } = DateTime.MinValue;

		public Usuario() { }

		public Usuario(int id, string name, string nickname, string email, string password, int functionId, DateTime birthDate, bool active, DateTime lastLogin)
			: base(id)
		{
			if (string.IsNullOrEmpty(name))
				throw new InvalidInsertedDataException(nameof(Name));
			if (string.IsNullOrEmpty(nickname))
				throw new InvalidInsertedDataException(nameof(Nickname));
			if (string.IsNullOrEmpty(email))
				throw new InvalidInsertedDataException(nameof(Email));
			if (string.IsNullOrEmpty(password))
				throw new InvalidInsertedDataException(nameof(Password));
			if (Enum.IsDefined(typeof(FunctionEnum), functionId))
				throw new InvalidInsertedDataException(nameof(FunctionId));
			if (birthDate == DateTime.MinValue || birthDate >= DateTime.Now)
				throw new InvalidInsertedDataException(nameof(BirthDate));

			Id = id;
			Name = name;
			Nickname = nickname;
			Email = email;
			Password = password;
			FunctionId = functionId;
			BirthDate = birthDate;
			Active = active;
			LastLogin = lastLogin;
		}
	}
}
