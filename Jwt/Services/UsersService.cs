using System.Linq;

namespace Jwt.Services
{
	public class UsersService : object, IUsersService
	{
		#region Static Member(s)
		private System.Collections.Generic.List<Models.User> _users;

		protected System.Collections.Generic.List<Models.User> Users
		{
			get
			{
				if (_users is null)
				{
					_users =
						new System.Collections.Generic.List<Models.User>();

					for (int index = 1; index <= 5; index++)
					{
						var user =
							new Models.User
							{
								Id = index,
								Password = "1234512345",
								Username = $"Username{ index }",
								LastName = $"Last Name { index }",
								FirstName = $"First Name { index }",
								EmailAddress = $"Sample{ index }@Gmail.com",
							};

						switch (index)
						{
							case 1:
							{
								user.Role = Models.Enums.UserType.Administrator;
								break;
							}
							case 2:
							{
								user.Role = Models.Enums.UserType.Owner;
								break;
							}
							case 3:
							{
								user.Role = Models.Enums.UserType.Developer;
								break;
							}
						}

						_users.Add(item: user);
					}
				}

				return _users;
			}
		}
		#endregion Static Member(s)

		#region Constructor(s)
		public UsersService
			(Microsoft.Extensions.Options.IOptions<Utilities.Settings.Main> options) : base()
		{
			Settings = options.Value;
		}
		#endregion /Constructor(s)

		#region Property(ies)
		protected Utilities.Settings.Main Settings { get; }
		#endregion /Property(ies)

		#region Get All
		public System.Collections.Generic.IList<Models.User> GetAll()
		{
			var foundedUsers = Users;

			//return Users;
			return foundedUsers;
		}
		#endregion /Get All

		#region Get All Async
		public async
			System.Threading.Tasks.Task
			<System.Collections.Generic.IList<Models.User>> GetAllAsync()
		{
			System.Collections.Generic.List<Models.User> foundedUsers = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				foundedUsers = Users;
			});

			return foundedUsers;
		}
		#endregion /Get All Async

		#region Get By Id
		public Models.User GetById(int id)
		{
			var foundedUser =
				Users
				.Where(current => current.Id == id)
				.FirstOrDefault();

			return foundedUser;
		}
		#endregion /Get By Id

		#region Get By Id Async
		public async
			System.Threading.Tasks.Task<Models.User>
			GetByIdAsync(int id)
		{
			Models.User foundedUser = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				foundedUser =
					Users
					.Where(current => current.Id == id)
					.FirstOrDefault();
			});

			return foundedUser;
		}
		#endregion /Get By Id Async

		#region Login
		public
			ViewModels.Users.LoginResponseViewModel
			Login(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			if (viewModel is null)
			{
				return null;
			}
			else if (string.IsNullOrWhiteSpace(value: viewModel.Password))
			{
				return null;
			}
			else if (string.IsNullOrWhiteSpace(value: viewModel.Username))
			{
				return null;
			}

			var foundedUser =
				Users
				.Where(current => current.Username.ToLower() == viewModel.Username.ToLower())
				.FirstOrDefault();

			if (foundedUser is null)
			{
				return null;
			}
			else if (string.Compare(strA: foundedUser.Password, strB: viewModel.Password, ignoreCase: false) != 0)
			{
				return null;
			}

			string token =
				Utilities.JwtUtility.GenerateJwtToken(user: foundedUser, setting: Settings);

			var response =
				new ViewModels.Users.LoginResponseViewModel(user: foundedUser, token: token);

			return response;
		}
		#endregion /Login

		#region Login Async
		public async
			System.Threading.Tasks.Task
			<ViewModels.Users.LoginResponseViewModel>
			LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			if (viewModel is null)
			{
				return null;
			}
			else if (string.IsNullOrWhiteSpace(value: viewModel.Password))
			{
				return null;
			}
			else if (string.IsNullOrWhiteSpace(value: viewModel.Username))
			{
				return null;
			}

			var foundedUser =
				Users
				.Where(current => current.Username.ToLower() == viewModel.Username.ToLower())
				.FirstOrDefault();

			if (foundedUser is null)
			{
				return null;
			}
			else if (string.Compare(strA: foundedUser.Password, strB: viewModel.Password, ignoreCase: false) != 0)
			{
				return null;
			}

			string token = await
				Utilities.JwtUtility.GenerateJwtTokenAsync(user: foundedUser, setting: Settings);

			var response =
				new ViewModels.Users.LoginResponseViewModel(user: foundedUser, token: token);

			return response;
		}
		#endregion /Login Async
	}
}
