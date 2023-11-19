using System.Text;
using System;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using XSystem.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using UserManagement.Application.Commands;
using UserManagement.Application.Dto;
using UserManagement.Application.Queries;
using UserManagement.HttpResult;

namespace UserManagement.Infrastructure.DB
{
    public class UserRepository : IUserRepository
    {
        private static string _userNotExist = "User Not Exist";
        private static string _userExist = "User Already Exist";
        private ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        public async Task<HttpResponse<CreateUserResult>> CreateUser(CreateUserCommand userCommand)
        {
            if (await _dbContext.Set<User>().AnyAsync(e => e.Email == userCommand.Email))
                return HttpResponse<CreateUserResult>.BadRequest(_userExist);
            var user = _mapper.Map<User>(userCommand);
            
            user.Id = GenerateUserId(user.Email);
            user.AccessToken = GenerateAccessToken(user.Id, user.Email);

            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();

            return HttpResponse<CreateUserResult>.Ok(_mapper.Map<CreateUserResult>(user));
        }

        private string GenerateUserId(string email)
        {
            var hashedEmail = ComputeSHA1Hash(email + Consts.GenerateUserIdSalt);
            return hashedEmail;
        }

        public async Task<HttpResponse<UserDTO>> GetUser(GetUserQuery getUserQuery)
        {
            var user = await _dbContext.Set<User>().FindAsync(getUserQuery.Id);

            if (user != null && user.AccessToken == getUserQuery.AccessToken)
            {
                // Omit email if marketingConsent is false
                var userDto = _mapper.Map<UserDTO>(user);
                if (!user.MarketingConsent)
                    userDto.Email = null;

                return HttpResponse<UserDTO>.Ok(userDto);
            }

            return HttpResponse<UserDTO>.BadRequest(_userNotExist);
        }

        private string GenerateAccessToken(String userId, string email)
        {
            var secretKey = _configuration["JWT:secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audiance"],
                signingCredentials: credentials,
                claims: new[]
                {
                    new Claim("userId", userId),
                    new Claim("email", email),
                    // If for example we can have email duplicated for softdeleted this make the access token not duplicated at all
                    new Claim("Id", Guid.NewGuid().ToString()),
                }
            );


            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private string ComputeSHA1Hash(string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
