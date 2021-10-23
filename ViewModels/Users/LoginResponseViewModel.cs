namespace ViewModels.Users
{
	public class LoginResponseViewModel : object
	{
		public LoginResponseViewModel(Models.User user, string token) : base()
		{
			if (user is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(user));
			}
			else if (string.IsNullOrWhiteSpace(value: token))
			{
				throw new System.ArgumentNullException(paramName: nameof(token));
			}

			Id = user.Id;

			Token = token;

			LastName = user.LastName;

			FirstName = user.FirstName;

			Username = user.Username;

			UserType = user.Role.ToString();
		}

		public int Id { get; set; }

		public string Token { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Username { get; set; }

		public string UserType { get; set; }
	}
}
