using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
	public class Startup : object
	{
		#region Constructor(s)
		public Startup
			(Microsoft.Extensions.Configuration.IConfiguration configuration) : base()
		{
			Configuration = configuration;
		}
		#endregion /Constructor(s)

		#region Property(ies)
		protected Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
		#endregion /Property(ies)

		#region Configure Services
		public void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddCors();

			services.AddControllers();

			services.Configure<Jwt.Utilities.Settings.Main>
				(Configuration.GetSection(key: "ApplicationSettings"));

			#region Swagger
			services.AddSwaggerGen(current =>
			{
				current.SwaggerDoc(
					name: "v1",
					info: new Microsoft.OpenApi.Models.OpenApiInfo
					{
						Title = "Api",
						Version = "v1",
						Contact =
							new Microsoft.OpenApi.Models.OpenApiContact
							{
								Name = "Reza Qadimi",
								Email = "RezaQadimi.ir@Gmail.com",
								Url = new System.Uri("https://Reza-Qadimi.ir"),
							},
					});

				current.AddSecurityDefinition(name: "Bearer", securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Scheme = "Bearer",
					Name = "Authorization",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
				});

				current.AddSecurityRequirement(securityRequirement: new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
				{
					{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
							Name ="Bearer",

							Scheme = "oauth2",

							In =
								Microsoft.OpenApi.Models.ParameterLocation.Header,

							Reference =
								new Microsoft.OpenApi.Models.OpenApiReference
								{
									Id = "Bearer",
									Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
								},
						},

						new System.Collections.Generic.List<string>()
					}
				});
			});
			#endregion /Swagger

			Core.DependencyContainer.ConfigureServices
				(configuration: Configuration, services: services);
		}
		#endregion /Configure Services

		#region Configure
		public void Configure
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app,
			Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseSwagger();

				app.UseSwaggerUI(current => current.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(current => current
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseJwtMiddleware();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
		#endregion /Configure
	}
}
