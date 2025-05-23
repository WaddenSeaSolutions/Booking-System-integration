using User_Microservice.Applications.Interfaces;
using User_Microservice.Domain.DTO;
using User_Microservice.Domain.Models;
using User_Microservice.Infrastructure.Repositories;

namespace User_Microservice.Domain.Services;

public class UserService: IUserService
{

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User Register(User user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;

            _userRepository.RegisterUser(user);

            return user;
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return user;
        }
}
