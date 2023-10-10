using ead_rest_project.Dtos;
using ead_rest_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ead_rest_project.services
{
    public interface IAuthService
    {
        RegisterResponse createUser(RegisterRequest request);
        LoginResponse login(LoginRequest request);
        List<ApplicationUser> getAllUsers();
        ApplicationUser getUser(string userId);
        ResponseDto reActivateUser(string userId);
    }
}
