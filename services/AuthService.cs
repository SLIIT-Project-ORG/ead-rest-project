using BCrypt.Net;
using ead_rest_project.Dtos;
using ead_rest_project.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
            Optional<ApplicationUser> optUser = _users.Find(ApplicationUser => ApplicationUser.username.Equals(request.username)).FirstOrDefault();

            if (!IsValidLoginRequest(request))
            {
                Console.WriteLine("Login Request invalided");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid login request"
                };
            }

            // Replace this with your own logic to check if the user exists in your database
            var user = optUser.Value;

            if (user == null)
            {
                Console.WriteLine("User not found");
                return new LoginResponse
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Verify the password
            if (!VerifyPassword(request.password, user.password))
            {
                Console.WriteLine("Invalid Password");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }
            
            Console.WriteLine("Token Generation part..");
            var accessToken = GenerateAccessToken(user.userId);

            return new LoginResponse
            {
                Success = true,
                AccessToken = accessToken,
                Email = user.email,
                UserId = user.userId,
                Message = "Login successful"
            };
        }

        private bool IsValidLoginRequest(LoginRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.username) && !string.IsNullOrWhiteSpace(request.password);
        }

        private bool VerifyPassword(string enteredPassword, string passwordHash)
        {
            bool isVerified = BCrypt.Net.BCrypt.Verify(enteredPassword, passwordHash);
            return isVerified;
        }

        private byte[] GenerateRandomKey(int keyLengthInBytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[keyLengthInBytes];
                rng.GetBytes(key);
                return key;
            }
        }

        private string GenerateAccessToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();


            byte[] secret_key = GenerateRandomKey(128);
            var key = new SymmetricSecurityKey(secret_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId)}),
                Expires = DateTime.UtcNow.AddHours(1), // Adjust token expiration as needed
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public List<ApplicationUser> getAllUsers()
        {
            List<ApplicationUser> applicationUsers = _users.Find(ApplicationUser => true).ToList();
            if(applicationUsers.Count == 0)
            {
                throw new Exception("Application not found!");
            }
            else
            {
                return applicationUsers;
            }
        }

        public ApplicationUser getUser(string userId)
        {
            if(userId == null)
            {
                throw new Exception("userId cannot be null");
            }
            else
            {
                Optional<ApplicationUser> optUser = _users.Find(ApplicationUser => ApplicationUser.userId.Equals(userId)).FirstOrDefault();
                if (!optUser.HasValue)
                {
                    throw new Exception("User not found!");
                }
                else
                {
                    return optUser.Value;
                }
            }
        }

        public ResponseDto reActivateUser(string userId)
        {
            ApplicationUser user = getUser(userId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                ResponseDto responseDto = new ResponseDto();
                user.isActive = true;
                _users.FindOneAndReplaceAsync(userId, user);

                responseDto.Success = true;
                responseDto.Message = "User re-activated";
                return responseDto;
            }
        }
    }
}