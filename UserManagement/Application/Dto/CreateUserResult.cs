using UserManagement.Entities;
using UserManagement.ProfileMapping;

namespace UserManagement.Application.Dto
{
    public class CreateUserResult: IMapFrom<User>
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
    }
}
