/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Controller class
 Filename        : AuthenticationController.cs
 Usage           : For define API endpoints
*/

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
        //Read only instance from IAuthService
        private readonly IAuthService iAuthService;
		
		//Constructor
		public AuthenticationController(IAuthService iAuthService)
		{
			this.iAuthService = iAuthService;
		}


        // Register Method (POST Request)
        //Endpoint => {BASE-URL}/api/v1/authenticate/register
        [HttpPost]
		[Route("/authenticate/register")]
		public ActionResult<RegisterResponse> Register([FromBody] RegisterRequest request)
		{
			return iAuthService.createUser(request);
		}


        //Login Method (POST Request)
        //Endpoint => {BASE-URL}/api/v1/authenticate/login
        [HttpPost]
		[Route("/authenticate/login")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
		public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
		{
			return iAuthService.login(request);
		}


        //Get all user Method (GET Request)
        //Endpoint => {BASE-URL}/api/v1/user/all
        [HttpGet]
		[Route("/user/all")]
		public ActionResult<List<ApplicationUser>> getAllUsers()
		{
			return iAuthService.getAllUsers();
        }


        //Get user by ID Method (GET Request)
        //Endpoint => {BASE-URL}/api/v1/user/{userId}
        [HttpGet]
		[Route("/user/{userId}")]
		public ActionResult<ApplicationUser> getUser(String userId)
		{
			return iAuthService.getUser(userId);
        }


        //Update user account Method (PUT Request)
        //Endpoint => {BASE-URL}/api/v1/user/reactivate/{userId}
        [HttpPut]
		[Route("/user/reactivate/{userId}")]
		public ActionResult<ResponseDto> reActivateUser(String userId)
		{
			return iAuthService.reActivateUser(userId);
        }

        [HttpPut("/user/{id}")]
        public void UpdateTrain(string id, [FromBody] ApplicationUser applicationUser)
        {
            iAuthService.UpdateUser(id, applicationUser);
        }

        [HttpDelete("/user/{id}")]
        public void DeleteUser(string id)
        {
            iAuthService.DeleteUser(id);
        }

    }
}