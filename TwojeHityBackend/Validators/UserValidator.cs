using TwojeHity.Repositories;
using System.Diagnostics.Eventing.Reader;
using TwojeHity.Models;
using TwojeHity.Exceptions;

namespace TwojeHity.Validators
{

    public interface IUserValidator
    {
        Task<bool> RegisterUserValidate(User user);

        void IsNull(User user);

        bool CheckPassword(string password);

    }
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository userRepository;


        public UserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> RegisterUserValidate(User user)
        {
            IsNull(user);
            bool loginexist = await userRepository.LoginExist(user);
            if(loginexist)
            {
                throw new CustomException("Istnieje już taki użytkownik");
            }
            CheckPassword(user.PasswordHash);
            return true;

        }

        public void IsNull(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < 6 || password == null)
            {
                throw new Exception();
            }
            return true;
        }
    }
}
