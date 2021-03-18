using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Sql.Employees
{
    internal class EmployeeDto : IUnwrap<Employee>, IWrap<Employee>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte[] Image { get; set; } = new byte[0];
        public string OrganizationId { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public Employee Unwrap()
        {
            return new Employee
            {
                Id = Id,
                Name = Name,
                Title = Title,
                Image = new Image(Image),
                Pin = Pin,
                Color = Color,
                OrganizationId = OrganizationId,
            };
        }

        public void Wrap(Employee entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
