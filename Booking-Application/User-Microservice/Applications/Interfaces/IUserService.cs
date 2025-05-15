using User_Microservice.Domain.DTO;
using User_Microservice.Domain.Models;

namespace User_Microservice.Applications.Interfaces;

public interface IUserService
{
    User Register(User user);
    User Login(string username, string password);
    User Logout(UserDTO user);
}