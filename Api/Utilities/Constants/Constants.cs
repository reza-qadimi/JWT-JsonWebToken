namespace Api.Utilities.Constants
{
	public static class Constant : object
	{
		static Constant()
		{
		}

		public static class Routing : object
		{
			static Routing()
			{
			}

			public const string Controller = "[controller]";
		}

		public static class UserType : object
		{
			static UserType()
			{
			}

			public const string User = nameof(Models.Enums.UserType.User);

			public const string Owner = nameof(Models.Enums.UserType.Owner);

			public const string Developer = nameof(Models.Enums.UserType.Developer);

			public const string Administrator = nameof(Models.Enums.UserType.Administrator);
		}
	}
}
