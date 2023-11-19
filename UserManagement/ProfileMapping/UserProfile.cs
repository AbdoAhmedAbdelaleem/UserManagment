using AutoMapper;
using UserManagement.Application.Commands;
using UserManagement.Application.Dto;
using UserManagement.Entities;

namespace UserManagement.ProfileMapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<CreateUserResult, User>().ReverseMap();
        }
    }
}
