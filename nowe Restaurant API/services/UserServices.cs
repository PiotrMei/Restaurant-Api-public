using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using nowe_Restaurant_API.Entities;
using nowe_Restaurant_API.Exceptions;
using nowe_Restaurant_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace nowe_Restaurant_API.services
{
    public class UserServices : IUserServices
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSetting;

        public UserServices(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSetting)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSetting = authenticationSetting;
        }

        public void CreateUser(UserDto userDto)
        {
            //var newuser = mapper
            //    .Map<User>(userDto);
            var newuser = new User()
            {
                Email = userDto.Email,
                Birthdate = userDto.Birthdate,
                Nationality = userDto.Nationality,
               
                RoleId = userDto.RoleId,
            };
            var hashedpassword = passwordHasher.HashPassword(newuser, userDto.Password);
            newuser.PasswordHash = hashedpassword;

            
            dbContext.Users.Add(newuser);
            dbContext.SaveChanges();
            
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            var logginguser = dbContext
                .Users
                .Include(a => a.Role)
                .FirstOrDefault(u => u.Email == loginUserDto.Email);
            if (logginguser == null)
            {
                throw new InvalidLoginExepction("Nieprawidłowy Email lub Hasło");
            }
            var passwordhash = passwordHasher.VerifyHashedPassword(logginguser, logginguser.PasswordHash, loginUserDto.Password);
            if (passwordhash == PasswordVerificationResult.Failed)
            {
                throw new InvalidLoginExepction("Nieprawidłowy Email lub Hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, logginguser.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{logginguser.FirstName} {logginguser.LastName}"),
                new Claim(ClaimTypes.Role, $"{logginguser.Role.Name}"),
                new Claim("Birthdate", logginguser.Birthdate.Value.ToString("yyyy-MM-dd")),
                
            };

            if (!logginguser.Nationality.IsNullOrEmpty())
            {
                claims.Add(new Claim("Nationality", logginguser.Nationality));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms .HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSetting.JwtExpireDays);
            var token = new JwtSecurityToken(authenticationSetting.JwtIssuer,
                authenticationSetting.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenhandler = new JwtSecurityTokenHandler();
            return tokenhandler.WriteToken(token);
        }
    }
}
