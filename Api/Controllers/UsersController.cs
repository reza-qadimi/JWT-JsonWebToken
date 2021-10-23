namespace Api.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public class UsersController : Utilities.ControllerBase
	{
		#region Constructor(s)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="usersService"></param>
		public UsersController(Jwt.Services.IUsersService usersService) : base()
		{
			UsersService = usersService;
		}
		#endregion /Constructor(s)

		#region Property(ies)
		/// <summary>
		/// 
		/// </summary>
		protected Jwt.Services.IUsersService UsersService { get; }
		#endregion /Property(ies)

		#region Login Async
		#region Document
		/// <summary>
		/// 
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(ViewModels.Users.LoginResponseViewModel),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(string), statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		#endregion /Document
		[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<ViewModels.Users.LoginResponseViewModel>>
			LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			var response =
				await UsersService.LoginAsync(viewModel: viewModel);

			if (response is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Errors.InvalidInformations,
					Resources.DataDictionary.Username, Resources.DataDictionary.Password);

				return BadRequest(error: errorMessage);
			}

			return Ok(value: response);
		}
		#endregion /Login Async

		#region Get All Async
		#region Document
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(System.Collections.Generic.IList<Models.User>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
		#endregion /Document
		[Jwt.Utilities.Attributes.Authorize(roles:
			Utilities.Constants.Constant.UserType.Owner +
			Utilities.Constants.Constant.UserType.Developer)]

		[Microsoft.AspNetCore.Mvc.HttpGet]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IList<Models.User>>>
			GetAllAsync()
		{
			var response =
				await UsersService.GetAllAsync();

			return Ok(value: response);
		}
		#endregion Get All Async

		#region Get By Id Async
		#region Document
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Models.User),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(string),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
		#endregion /Document
		// Strongly Typed: Adminstrator, Develper...
		[Jwt.Utilities.Attributes.Authorize(roles:
			Utilities.Constants.Constant.UserType.Owner +
			Utilities.Constants.Constant.UserType.Developer +
			Utilities.Constants.Constant.UserType.Administrator)]

		[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id?}")]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IList<Models.User>>>
			GetByIdAsync(int? id)
		{
			if (id.HasValue is false)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.ArgumentNullException,
					Resources.DataDictionary.Id);

				return BadRequest(error: errorMessage);
			}
			else
			{
				var response =
					await UsersService.GetByIdAsync(id: id.Value);

				return Ok(value: response);
			}
		}
		#endregion Get By Id Async
	}
}
