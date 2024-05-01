namespace SuperHeroApp.Server.Models
{
	public class ResultModel
	{
		public bool Success { get; set; } = false;
		public string Message { get; set; } = string.Empty;
	}
	public class ResultModel<T>
	{
		public bool Success { get; set; } = false;
		public string Message { get; set; } = string.Empty;
		public T? Result { get; set; }
	}

}
