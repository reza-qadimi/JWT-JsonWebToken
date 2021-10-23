namespace Jwt.Services
{
	public interface IUsersService
	{
		#region Get By Id
		Models.User GetById(int id);

		System.Threading.Tasks.Task
			<Models.User> GetByIdAsync(int id);
		#endregion /Get By Id

		#region Get All
		System.Collections.Generic.IList<Models.User> GetAll();

		System.Threading.Tasks.Task
			<System.Collections.Generic.IList<Models.User>> GetAllAsync();
		#endregion /Get All

		#region Login
		ViewModels.Users.LoginResponseViewModel
			Login(ViewModels.Users.LoginRequestViewModel viewModel);

		System.Threading.Tasks.Task
			<ViewModels.Users.LoginResponseViewModel>
			LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel);
		#endregion Login
	}
}
