using System.Linq;

namespace Jwt.Utilities.Middlewares
{
	public class JwtMiddleware : object
	{
		#region Constructor(s)
		public JwtMiddleware
			(Microsoft.AspNetCore.Http.RequestDelegate next,
			Microsoft.Extensions.Options.IOptions<Settings.Main> options) : base()
		{
			Next = next;
			Settings = options.Value;
		}
		#endregion /Constructor(s)

		#region Property(ies)
		protected Settings.Main Settings { get; }

		protected Microsoft.AspNetCore.Http.RequestDelegate Next { get; }
		#endregion /Property(ies)

		#region Invoke async
		public async
			System.Threading.Tasks.Task InvokeAsync
			(Microsoft.AspNetCore.Http.HttpContext context, Services.IUsersService usersService)
		{
			var requestHeaders =
				context.Request.Headers[Constants.Shared.Authorization];

			string token =
				requestHeaders
				.FirstOrDefault()
				?.Split(" ")
				.Last();

			if (string.IsNullOrWhiteSpace(value: token) is false)
			{
				await JwtUtility.AttachUserToContextAsync
					(token: token, context: context, usersService: usersService,secretKey: Settings.SecretKey);
			}

			await Next(context: context);
		}
		#endregion /Invoke async
	}
}
