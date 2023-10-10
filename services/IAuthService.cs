using ead_rest_project.Dtos;

namespace ead_rest_project.services
{
    public interface IAuthService
    {
        RegisterResponse createUser(RegisterRequest request);
        LoginResponse login(LoginRequest request);

    }
}
