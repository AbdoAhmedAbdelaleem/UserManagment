using AutoMapper;
using UserManagement.Entities;
using UserManagement.ProfileMapping;

namespace UserManagement.Application.Commands
{
    public class CreateUserCommand : IMapFrom<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool MarketingConsent { get; set; }
    }
}
