using Microsoft.AspNetCore.Builder;

//namespace Api.Utilities.Middlewares
namespace Microsoft.Extensions.DependencyInjection
{
	public static class MiddlewareExtensions : object
	{
		#region Constructor(s)
		static MiddlewareExtensions()
		{
		}
		#endregion /Constructor(s)

		#region Use Jwt Middleware
		public static
			Microsoft.AspNetCore.Builder.IApplicationBuilder
			UseJwtMiddleware(this Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
		{
			// UseMiddleware → Extension Method → using Microsoft.AspNetCore.Builder;
			var result =
				builder.UseMiddleware<Jwt.Utilities.Middlewares.JwtMiddleware>();

			return result;
		}
		#endregion /Use Jwt Middleware
	}
}
