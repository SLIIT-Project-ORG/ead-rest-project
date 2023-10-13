/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Dto
 Filename        : RegisterResponse.cs
 Usage           : For response
*/

namespace ead_rest_project.Dtos
{
	public class RegisterResponse
	{
		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }

	}
}
