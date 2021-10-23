using Microsoft.Extensions.DependencyInjection;

namespace Core
{
	public static class DependencyContainer : object
	{
		static DependencyContainer()
		{
		}

		public static void ConfigureServices
			(Microsoft.Extensions.Configuration.IConfiguration configuration,
			Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddTransient
				<Microsoft.AspNetCore.Http.IHttpContextAccessor,
				Microsoft.AspNetCore.Http.HttpContextAccessor>();

			services.AddScoped<Jwt.Services.IUsersService, Jwt.Services.UsersService>();
		}
	}
}
