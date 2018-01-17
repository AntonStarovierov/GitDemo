using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreService.Base.Auth
{
	public class AuthOptions
	{
		public const string Issuer = "BookstoreServer";
		public const string Audience = "BookstoreServices";
		private const string Key = "privateKeyForBookstoreServices";
		public const int Lifetime = 60; //60 minutes
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			var a = 1;
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
		}
	}
}
