using System;
using System.Collections.Generic;
using System.Text;

namespace Settings.Alert
{
	public class PutAlert
	{
		public string GetAlert(string title, string body, string type)
		{
			return string.Concat("swal(\"", title, "!\", \"", body, "\", \"", type, "\");");
		}
	}
}
