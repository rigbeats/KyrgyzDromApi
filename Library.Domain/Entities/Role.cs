using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
	public class Role : EntityBase
	{
		[MaxLength(50)]
		public string Title { get; set; }
	}
}
