/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Dto
 Filename        : RegisterResponse.cs
 Usage           : For response
*/

namespace ead_rest_project.Dtos
{
	public class LoginResponse
	{
		public bool Success { get; set; }
		public string AccessToken { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;

		public string Message {  get; set; } = string.Empty;


	}
}
