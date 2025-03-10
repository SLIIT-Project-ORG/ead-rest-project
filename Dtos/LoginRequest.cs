﻿/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Dto
 Filename        : LoginRequest.cs
 Usage           : For request
*/

using System.ComponentModel.DataAnnotations;

namespace ead_rest_project.Dtos
{
	public class LoginRequest
	{
		[Required]
		public string username { get; set; } = string.Empty;

		[Required,DataType(DataType.Password)]
		public string password { get; set; } = string.Empty;
	}
}
