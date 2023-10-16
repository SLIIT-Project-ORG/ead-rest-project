/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Service Class
 Filename        : AuthService.cs
 Usage           : For Service Implementation
*/

using BCrypt.Net;
using ead_rest_project.Dtos;
using ead_rest_project.enums;
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
        //Read only user collection instance
        private readonly IMongoCollection<ApplicationUser> _users;

        //Constructor for AuthService class
        public AuthService(AuthStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            //Get database connection detaills from appSettings file and AuthStoreDatabaseSettings file
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<ApplicationUser>(settings.UserCollectionName);
        }

        //Create user method implementation
        public RegisterResponse createUser(RegisterRequest request)
        {
            try
            {
                //Check NIC is null or empty
                if (request.nic == null || request.nic.Equals(""))
                {
                    throw new Exception("NIC cannot be null");
                }

                //Get all the users from user collection
                List<ApplicationUser> userList = _users.Find(ApplicationUser => true).ToList();
                foreach (ApplicationUser user in userList)
                {
                    //Checking if there is already a user for the given NIC
                    if (user.nic.Equals(request.nic))
                    {
                        throw new Exception($"{user.nic} was already in the system.");
                    }
                }

                //Create new applicatonUser object for save to DB
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

                //Password encryption using BCrypt
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
                //Set encrypted password to applicationUser object
                applicationUser.password = hashPassword;

                try
                {
                    //save applicationUser object to DB
                    _users.InsertOne(applicationUser);
                    Console.WriteLine("New User Created..");
                    //Set response message and success status
                    RegisterResponse registerResponse = new RegisterResponse();
                    registerResponse.Success = true;
                    registerResponse.Message = "New User Created";
                    //return
                    return registerResponse;
                }
                catch (Exception e)
                {
                    //Exception handle using try catch
                    throw new Exception(e.Message);
                }
            }catch(Exception e)
            {
                RegisterResponse response = new RegisterResponse();
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
        }

        //User Login method implementation
        public LoginResponse login(LoginRequest request)
        {
            //Finding user in the system with given username in the request.
            Optional<ApplicationUser> optUser = _users.Find(ApplicationUser => ApplicationUser.username.Equals(request.username)).FirstOrDefault();

            //Check login request valid or not.
            if (!IsValidLoginRequest(request))
            {
                Console.WriteLine("Login Request invalided");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid login request"
                };
            }

            //Set to user, if user exist in the system
            var user = optUser.Value;

            //Check user available or not
            if (user == null)
            {
                Console.WriteLine("User not found");
                return new LoginResponse
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Compare request password and encrypted password
            if (!VerifyPassword(request.password, user.password))
            {
                Console.WriteLine("Invalid Password");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }
            
            //JWT token generation
            Console.WriteLine("Token Generation part..");
            var accessToken = GenerateAccessToken(user.userId);

            //Response object for successful login
            return new LoginResponse
            {
                Success = true,
                AccessToken = accessToken,
                Email = user.email,
                UserId = user.userId,
                RoleId = user.roleId,
                Message = "Login successful"
            };
        }

        //Custom method implementation for checking valid login
        private bool IsValidLoginRequest(LoginRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.username) && !string.IsNullOrWhiteSpace(request.password);
        }

        //Custom method implementation for Password verification.
        private bool VerifyPassword(string enteredPassword, string passwordHash)
        {
            //Compare passwords using BCrypt
            bool isVerified = BCrypt.Net.BCrypt.Verify(enteredPassword, passwordHash);
            return isVerified;
        }

        //Random key generate for Secret key
        private byte[] GenerateRandomKey(int keyLengthInBytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                //key
                byte[] key = new byte[keyLengthInBytes];
                rng.GetBytes(key);
                return key;
            }
        }

        private string GenerateAccessToken(string userId)
        {
            //JWT Token handler object
            var tokenHandler = new JwtSecurityTokenHandler();

            //Set generated random byte[] to secret key
            byte[] secret_key = GenerateRandomKey(128);
            var key = new SymmetricSecurityKey(secret_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //set userId, expiration time and algorithm to token
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId)}),
                Expires = DateTime.UtcNow.AddHours(1), // Adjust token expiration as needed
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            //create JWT
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //User get all method implementation
        public List<ApplicationUser> getAllUsers()
        {
            //Get user list from the table
            List<ApplicationUser> applicationUsers = _users.Find(ApplicationUser => true).ToList();
            //check user list empty or not
            if(applicationUsers.Count == 0)
            {
                throw new Exception("Application not found!");
            }
            else
            {
                return applicationUsers;
            }
        }

        //User getById method implementation
        public ApplicationUser getUser(string userId)
        {
            //checking userId null or not
            if(userId == null)
            {
                throw new Exception("userId cannot be null");
            }
            else
            {
                //Checking user in ther DB
                Optional<ApplicationUser> optUser = _users.Find(ApplicationUser => ApplicationUser.userId.Equals(userId)).FirstOrDefault();
                if (!optUser.HasValue)
                {
                    throw new Exception("User not found!");
                }
                else
                {
                    //return selected user
                    return optUser.Value;
                }
            }
        }

        public ResponseDto reActivateUser(string userId)
        {
            //Select the user by userId using getUser() method
            ApplicationUser user = getUser(userId);
            if(user == null)
            {
                //user not found
                throw new Exception("User not found");
            }
            else
            {
                //If user exist
                ResponseDto responseDto = new ResponseDto();
                //Set isActive boolean to true
                if (user.isActive == true)
                {
                    user.isActive = false;
                    responseDto.Message = "User De-activated";
                }
                else
                {
                    user.isActive = true;
                    responseDto.Message = "User Re-activated";
                }
                //Save updated user details
                _users.FindOneAndReplaceAsync(T => T.userId == userId, user);

                //Set message and success status to response
                responseDto.Success = true;
                return responseDto;
            }
        }

        public void UpdateUser(string id, ApplicationUser applicationUser)
        {
            if(id == null || id.Equals("")){
                throw new Exception("Id cannot be null");
            }
            _users.FindOneAndReplaceAsync(a => a.userId == id, applicationUser);
            Console.WriteLine("User updated");
        }

        public void DeleteUser(string id)
        {
            if(id == null || id.Equals(""))
            {
                throw new Exception("Id cannot be null");
            }
            Optional<ApplicationUser> optUser = _users.Find(ApplicationUser => ApplicationUser.userId.Equals(id)).FirstOrDefault();
            if (optUser.HasValue != true)
            {
                throw new Exception("User not found for given userId");
            }
            else
            {
                //If exist, Delete train by given trainId
                _users.DeleteOne(ApplicationUser => ApplicationUser.userId.Equals(id));
                Console.WriteLine("User details deleted..");
            }
        }
    }
}