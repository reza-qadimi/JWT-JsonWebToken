using System.Linq;

namespace Jwt.Utilities
{
	public class JwtUtility : object
	{
		public JwtUtility() : base()
		{
		}

		#region Generate Token
		public static
			string
			GenerateJwtToken(Models.User user, Settings.Main setting)
		{
			byte[] key =
				System.Text.Encoding.ASCII.GetBytes(s: setting.SecretKey);

			var symmetricSecurityKey =
				new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);

			var securityAlgorithm =
				Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;

			var signingCredentials = new Microsoft.IdentityModel.Tokens
				.SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

			var tokenDescriptor =
				new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
				{
					Subject = new System.Security.Claims.ClaimsIdentity(claims: new[]
					{
						new System.Security.Claims.Claim(type: nameof(user.FirstName), value: user.FirstName),
						new System.Security.Claims.Claim(type: nameof(user.FirstName), value: user.FirstName),
						new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Name, value: user.Username),
						new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Email, value: user.EmailAddress),
						new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Role, value: user.Role.ToString()),
						new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.NameIdentifier, value: user.Id.ToString()),
					}),

					//Issuer = string.Empty,

					//Audience = string.Empty,

					SigningCredentials = signingCredentials,

					Expires = System.DateTime.UtcNow.AddMinutes(value: setting.TokenExpiresInMinutes),
				};

			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			var securityToken =
				tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

			string token =
				tokenHandler.WriteToken(token: securityToken);

			return token;
		}
		#endregion /Generate Token

		#region Generate Token
		public static
			async
			System.Threading.Tasks.Task<string>
			GenerateJwtTokenAsync(Models.User user, Settings.Main setting)
		{
			string token = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				token = GenerateJwtToken(user: user, setting: setting);
			});

			return token;
		}
		#endregion /Generate Token

		#region Get User Id Claim
		public static
			System.Security.Claims.Claim
			GetUserIdClaim(string token, string secretKey)
		{
			byte[] key =
				System.Text.Encoding.ASCII.GetBytes(s: secretKey);

			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			tokenHandler.ValidateToken(
				token: token, validationParameters:
				new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = false,

					ValidateAudience = false,

					ValidateIssuerSigningKey = true,

					ClockSkew = System.TimeSpan.Zero,

					IssuerSigningKey =
						new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key),

				}, validatedToken: out Microsoft.IdentityModel.Tokens.SecurityToken validateToken);


			if (validateToken is not System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtToken)
			{
				return null;
			}

			var userIdClaim =
				jwtToken.Claims
				.Where(current => current.Type.ToLower() == "NameId".ToLower())
				.FirstOrDefault();

			return userIdClaim;
		}
		#endregion /Get User Id Claim

		#region Attach User To Context
		public static
			void
			AttachUserToContext(
			string token, string secretKey,
			Services.IUsersService usersService,
			Microsoft.AspNetCore.Http.HttpContext context)
		{
			try
			{
				var currentClaim =
					GetUserIdClaim(token: token, secretKey: secretKey);

				if (currentClaim is null)
				{
					return;
				}

				var userId =
					int.Parse(s: currentClaim.Value);

				var foundedUser =
					usersService.GetById(id: userId);

				if (foundedUser is null)
				{
					return;
				}

				context.Items[Constants.Shared.User] = foundedUser;
			}
			catch
			{
			}
		}
		#endregion /Attach User To Context

		#region Attach User To Context
		public static
			async
			System.Threading.Tasks.Task
			AttachUserToContextAsync(
			string token, string secretKey,
			Services.IUsersService usersService,
			Microsoft.AspNetCore.Http.HttpContext context)
		{
			try
			{
				var currentClaim =
					GetUserIdClaim(token: token, secretKey: secretKey);

				if (currentClaim is null)
				{
					return;
				}

				var userId =
					int.Parse(s: currentClaim.Value);

				var foundedUser =
					await usersService.GetByIdAsync(id: userId);

				if (foundedUser is null)
				{
					return;
				}

				context.Items[Constants.Shared.User] = foundedUser;
			}
			catch
			{
			}
		}
		#endregion /Attach User To Context
	}
}
