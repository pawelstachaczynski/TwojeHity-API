using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwojeHity.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwojeHity.Models;
using TwojeHity.Models.DTOs.User;
using TwojeHity.Settings;
using TwojeHity.Validators;
using TwojeHity.Exceptions;

namespace TwojeHity.Services
{

    public interface IAccountService
    {
        Task<AuthToken> GenerateJwtToken(LoginDto dto);
        Task RegisterUser(RegisterUserDto registerUserDto);
    }

    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserValidator _userValidator;

        public AccountService(IMapper mapper, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUserValidator userValidator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userValidator = userValidator;
        }

        public async Task<AuthToken> GenerateJwtToken(LoginDto dto)
        {
            if(dto.Login == "" || dto.Password == "")
            {
                throw new CustomException("Wypełnij wszystkie pola");
            }
            var user = await _userRepository.GetUserByLogin(dto.Login);
            if (user == null)
            {
                user = null;
                throw new CustomException("Nie ma takiego użytkownika");
            }
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                user = null;
                throw new CustomException("Login lub hasło jest niepoprawne");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var result = tokenHandler.WriteToken(token);
                var authToken = new AuthToken
                {
                    UserId = user.Id,
                    Login = user.Login,
                    Token = result,
                    TokenExpirationDate = expires
                };
                return authToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas pisania tokena JWT: {ex.Message}");
                return null;
            }

        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            var newUser = _mapper.Map<User>(registerUserDto);
            newUser.PasswordHash = registerUserDto.Password;
            await _userValidator.RegisterUserValidate(newUser);
            var hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = hashedPassword;

            await _userRepository.RegisterUser(newUser);
        }
    }
}
