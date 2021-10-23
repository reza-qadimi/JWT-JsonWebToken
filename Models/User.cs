namespace Models
{
	public class User : object
	{
		public User() : base()
		{
		}

		public int Id { get; set; }
		//public System.Guid Id { get; set; }

		public string Password { get; set; }

		public string Username { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string EmailAddress { get; set; }

		public Enums.UserType Role { get; set; }
	}
}
