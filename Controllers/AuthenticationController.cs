
using ead_rest_project.Dtos;
using ead_rest_project.services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ead_rest_project.Controllers
{
	[ApiController]
	[Route("api/v1/authenticate")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService iAuthService;
		public AuthenticationController(IAuthService iAuthService)
		{
			this.iAuthService = iAuthService;
		}

		//Add Role
	/*	[HttpPost]
		[Route("roles/add")]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
		{
			return null;
		}
	*/


		// Register Method

		[HttpPost]
		[Route("register")]
		public ActionResult<RegisterResponse> Register([FromBody] RegisterRequest request)
		{
			return iAuthService.createUser(request);
		}


		//Login Method
		[HttpPost]
		[Route("login")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
		public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
		{
			return iAuthService.login(request);
		}
	}
}