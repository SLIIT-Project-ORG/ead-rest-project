using ead_rest_project.Dtos;
using ead_rest_project.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ead_rest_project.services
{
    public class AuthService : IAuthService
    {

        private readonly IMongoCollection<ApplicationUser> _users;

        public AuthService(IAuthStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<ApplicationUser>(settings.UserCollectionName);
        }

        public RegisterResponse createUser(RegisterRequest request)
        {
            return null;
        }

        public LoginResponse login(LoginRequest request)
        {
            return null;
        }

    }
}
