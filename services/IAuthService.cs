/*
 Developed       : Sachini Rasanga (IT20046552)
 Function        : User Management
 Component Type  : Service interface
 Filename        : IAuthService.cs
 Usage           : For define methods
*/

using ead_rest_project.Dtos;
using ead_rest_project.Models;

namespace ead_rest_project.services
{
    public interface IAuthService
    {
        RegisterResponse createUser(RegisterRequest request);
        LoginResponse login(LoginRequest request);
        List<ApplicationUser> getAllUsers();
        ApplicationUser getUser(string userId);
        ResponseDto reActivateUser(string userId);
        void UpdateUser(string id, ApplicationUser applicationUser);
        void DeleteUser(string id);
    }
}
