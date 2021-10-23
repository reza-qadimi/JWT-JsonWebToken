namespace ViewModels.Users
{
	public class LoginRequestViewModel : object
	{
		public LoginRequestViewModel() : base()
		{
		}

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
