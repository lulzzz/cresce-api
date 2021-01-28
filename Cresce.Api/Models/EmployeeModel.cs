using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Api.Models
{
    public record EmployeeModel
    {
        public EmployeeModel(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Title = employee.Title;
            Image = employee.Image.ToBase64();
        }

        public EmployeeModel()
        {
        }

        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
    }
}
