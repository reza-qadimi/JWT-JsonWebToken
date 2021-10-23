using Dtat.Results;

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
			(type: typeof(Dtat.Results.Result<ViewModels.Users.LoginResponseViewModel>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Dtat.Results.Result<string>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		#endregion /Document
		[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<Dtat.Results.Result<ViewModels.Users.LoginResponseViewModel>>>
			LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			var returnValue = new FluentResults.Result
				<ViewModels.Users.LoginResponseViewModel>();

			var result =
				await UsersService.LoginAsync(viewModel: viewModel);

			if (result is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Errors.InvalidInformations,
					Resources.DataDictionary.Username,
					Resources.DataDictionary.Password);

				returnValue.WithError(errorMessage: errorMessage);

				return BadRequest(returnValue.ConvertToDtatResult());
			}
			else
			{
				returnValue.WithValue(value: result);

				return Ok(value: returnValue.ConvertToDtatResult());
			}
		}
		#endregion /Login Async

		#region Get All Async
		#region Document
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Dtat.Results.Result<System.Collections.Generic.IList<Models.User>>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
		#endregion /Document
		// Strongly Typed: Adminstrator, Develper...
		[Jwt.Utilities.Attributes.Authorize
			(roles: Utilities.Constants.UserType.Owner
			+ Utilities.Constants.UserType.Developer)]

		[Microsoft.AspNetCore.Mvc.HttpGet]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<Dtat.Results.Result
			<System.Collections.Generic.IList<Models.User>>>>
			GetAllAsync()
		{
			var returnValue = new FluentResults.Result
				<System.Collections.Generic.IList<Models.User>>();

			var result =
				await UsersService.GetAllAsync();

			returnValue.WithValue(value: result);

			return Ok(value: returnValue.ConvertToDtatResult());
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
			(type: typeof(Dtat.Results.Result<Models.User>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Dtat.Results.Result<string>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
		#endregion /Document
		// Strongly Typed: Adminstrator, Develper...
		[Jwt.Utilities.Attributes.Authorize
			(roles: Utilities.Constants.UserType.Owner
			+ Utilities.Constants.UserType.Developer +
			Utilities.Constants.UserType.Administrator)]

		[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id?}")]
		public async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<Dtat.Results.Result
			<System.Collections.Generic.IList<Models.User>>>>
			GetByIdAsync(int? id)
		{
			var returnValue =
				new FluentResults.Result<Models.User>();

			if (id.HasValue is false)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.ArgumentNullException,
					Resources.DataDictionary.Id);

				returnValue.WithError(errorMessage: errorMessage);

				return BadRequest(error: returnValue.ConvertToDtatResult());
			}
			else
			{
				var result =
					await UsersService.GetByIdAsync(id: id.Value);

				returnValue.WithValue(value: result);

				return Ok(value: returnValue.ConvertToDtatResult());
			}
		}
		#endregion Get By Id Async
	}
}

/*
{
	"username": "Username1",
	"password": "1234512345"
}

{
	"username": "Username2",
	"password": "1234512345"
}

{
	"username": "Username3",
	"password": "1234512345"
}

{
	"username": "Username4",
	"password": "1234512345"
}

{
	"username": "Username5",
	"password": "1234512345"
}
 */
