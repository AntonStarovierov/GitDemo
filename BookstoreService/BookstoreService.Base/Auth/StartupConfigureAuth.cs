using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreService.Base.Auth
{
	public static class StartupConfigureAuth
	{
		public static void ConfigureAuth(IApplicationBuilder app)
		{
			var a = 1;
			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = AuthOptions.Issuer,
					ValidateAudience = true,
					ValidAudience = AuthOptions.Audience,
					ValidateLifetime = true,
					IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
					ValidateIssuerSigningKey = true
				}
			});
		}
	}
}
