using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Entities;
using TrackerWebApi.Exceptions;
using TrackerWebApi.Helpers;
using TrackerWebApi.Infrastructure;
using TrackerWebApi.Model;

namespace TrackerWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ApplicationDbContext _context;

        public UserService(IOptions<AppSettings> appSettings, IPasswordHasher<User> hasher, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _hasher = hasher;
            _context = context;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == request.Username);

            if (user is null)
            {
                 throw new AuthenticationException("User with this name does not exist.");
            }

            var verificationResult = _hasher.VerifyHashedPassword(null, user.PasswordHash, request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Incorrect password.");
            }

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user.Id, token);
        }

        public RegistrationResponse Register(RegistrationRequest request)
        {
            var userAlreadyExists = _context.Users.FirstOrDefault(user => user.UserName == request.UserName) is not null;
            if (userAlreadyExists)
            {
                throw new RegistrationException("User already exists.");
            }

            var newUser = new User
            {
                UserName = request.UserName,
                PasswordHash = _hasher.HashPassword(null, request.Password)
            };

            var addedUser = _context.Add<User>(newUser).Entity;
            _context.SaveChanges();

            return new RegistrationResponse(addedUser.Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Single(x => x.Id == id.ToString());
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
