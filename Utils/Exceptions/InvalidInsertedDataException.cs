using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Exceptions
{
	public class InvalidInsertedDataException : Exception
	{
		public InvalidInsertedDataException(string message)
	   : base(string.Concat("O Campo ", message, " não pode estar vazio!"))
		{
		}
	}
}
