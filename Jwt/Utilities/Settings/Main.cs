namespace Jwt.Utilities.Settings
{
	public class Main : object
	{
		public Main() : base()
		{
		}

		public string SecretKey { get; set; }

		public uint TokenExpiresInMinutes { get; set; }
	}
}
