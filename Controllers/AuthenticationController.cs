
using ead_rest_project.Dtos;
using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ead_rest_project.Controllers
{
	[ApiController]
	[Route("api/v1")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService iAuthService;
		public AuthenticationController(IAuthService iAuthService)
		{
			this.iAuthService = iAuthService;
		}


		// Register Method
		[HttpPost]
		[Route("/authenticate/register")]
		public ActionResult<RegisterResponse> Register([FromBody] RegisterRequest request)
		{
			return iAuthService.createUser(request);
		}


		//Login Method
		[HttpPost]
		[Route("/authenticate/login")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
		public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
		{
			return iAuthService.login(request);
		}


		[HttpGet]
		[Route("/user/all")]
		public ActionResult<List<ApplicationUser>> getAllUsers()
		{
			return iAuthService.getAllUsers();
        }

		[HttpGet]
		[Route("/user/{userId}")]
		public ActionResult<ApplicationUser> getUser(String userId)
		{
			return iAuthService.getUser(userId);

        }

		[HttpPut]
		[Route("/user/reactivate/{userId}")]
		public ActionResult<ResponseDto> reActivateUser(String userId)
		{
			return iAuthService.reActivateUser(userId);
        }

	}
}