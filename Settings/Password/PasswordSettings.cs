using System;
using System.Collections.Generic;
using System.Text;

namespace Settings.Password
{
	public class PasswordSettings
	{
		public string NovaSenha()
		{
			Random r = new Random();
			return r.Next(1, 99999).ToString("D6");
		}
	}
}
