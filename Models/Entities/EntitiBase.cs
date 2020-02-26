using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities
{
	public abstract class EntitiBase
	{
		[Key]
		public int Id { get; set; }

		public EntitiBase() { }

		protected EntitiBase(int id)
		{
			Id = id;
		}
	}
}
