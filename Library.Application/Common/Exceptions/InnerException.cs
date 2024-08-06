namespace Library.Application.Common.Exceptions
{
	public class InnerException : Exception
	{
		public InnerException(string message)
			: base(message) { }
	}
}
