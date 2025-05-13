using User_Microservice.Domain.DTO;

namespace User_Microservice.Applications.Interfaces;

public interface IUserService
{
    bool Register(UserDTO user);
    bool Login(UserDTO user);
    bool Logout(UserDTO user);
}