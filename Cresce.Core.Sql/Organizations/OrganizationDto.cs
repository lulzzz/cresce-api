using Cresce.Core.Organizations;

namespace Cresce.Core.Sql.Organizations
{
    internal class OrganizationDto : IUnwrap<Organization>, IWrap<Organization>
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public Organization Unwrap()
        {
            return new Organization
            {
                Name = Id
            };
        }

        public void Wrap(Organization entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
