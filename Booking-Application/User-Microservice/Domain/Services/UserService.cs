using User_Microservice.Domain.Models;
using User_Microservice.Infrastructure.Repositories;

namespace User_Microservice.Domain.Services;

public class UserService
{

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User Register(string username, string email, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPassword
            };

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
