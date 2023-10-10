using ead_rest_project.Dtos;
using ead_rest_project.Models;
using MongoDB.Driver;
using System.Data.Common;

namespace ead_rest_project.services
{
    public class AuthService : IAuthService
    {

        private readonly IMongoCollection<ApplicationUser> _users;

        public AuthService(AuthStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<ApplicationUser>(settings.UserCollectionName);
        }

        public RegisterResponse createUser(RegisterRequest request)
        {
            if (request.nic == null || request.nic.Equals(""))
            {
                throw new Exception("NIC cannot be null");
            }

            List<ApplicationUser> userList = _users.Find(ApplicationUser => true).ToList();
            foreach (ApplicationUser user in userList)
            {
                if (user.nic.Equals(request.nic))
                {
                    throw new Exception($"{user.nic} was already in the system.");
                }
            }

            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.firstName = request.firstName;
            applicationUser.lastName = request.lastName;
            applicationUser.username = request.username;
            applicationUser.email = request.email;
            applicationUser.mobileNo = request.mobileNo;
            applicationUser.nic = request.nic;
            applicationUser.gender = request.gender;
            applicationUser.age = request.age;
            applicationUser.imageRef = request.imageRef;
            applicationUser.description = request.description;
            applicationUser.isActive = true;
            applicationUser.roleId = request.roleId;

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
            applicationUser.password = hashPassword;

            try
            {
                _users.InsertOne(applicationUser);
                Console.WriteLine("New User Created..");
                RegisterResponse registerResponse = new RegisterResponse();
                registerResponse.Success = true;
                registerResponse.Message = "New User Created";
                return registerResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public LoginResponse login(LoginRequest request)
        {
            return null;
        }

    }
}