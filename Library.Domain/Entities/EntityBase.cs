using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
	public class EntityBase
	{
		[MaxLength(36)]
		public string Id { get; set; }
	}
}
