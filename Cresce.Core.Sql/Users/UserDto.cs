using Cresce.Core.Users;

namespace Cresce.Core.Sql.Users
{
    internal class UserDto : IUnwrap<User>, IWrap<User>
    {
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User Unwrap()
        {
            if (Id == null)
            {
                return new UnknownUser();
            }

            return new AdminUser
            {
                Id = Id,
                Password = Password
            };
        }

        public void Wrap(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
