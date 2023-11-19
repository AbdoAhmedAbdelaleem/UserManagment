using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserManagement.Application.Commands;
using UserManagement.Application.Dto;
using UserManagement.Application.Queries;
using UserManagement.Entities;
using UserManagement.HttpResult;

namespace UserManagement.Application
{
    public interface IUserRepository
    {
        public Task<HttpResponse<UserDTO>> GetUser(GetUserQuery getUserQuery);
        public Task<HttpResponse<CreateUserResult>> CreateUser(CreateUserCommand user);
    }
}
