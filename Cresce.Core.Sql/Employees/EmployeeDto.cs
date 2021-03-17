using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Sql.Employees
{
    internal class EmployeeDto : IUnwrap<Employee>, IWrap<Employee>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string OrganizationId { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

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
