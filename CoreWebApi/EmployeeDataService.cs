using DataComponentLib;
using DataComponentLib.Data;

namespace CoreWebApi
{
    public interface IEmployeeDataService
    {
        Task AddEmployee(Employee employee);
        Task DeleteEmployeeById(int id);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task UpdateEmployee(Employee employee);
    }

    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeDataService(IEmployeeService service) { _employeeService = service; }

        public Task<List<Employee>> GetAllEmployees()
        {
            return Task<List<Employee>>.Run(() =>
            {
                var employees = _employeeService.GetAllEmployees();
                return employees;
            });
        }

        public Task<Employee> GetEmployeeById(int id)
        {
            return Task<Employee>.Run(() =>
            {
                var emp = _employeeService.GetEmployeeById(id);
                return emp;
            });
        }

        public Task DeleteEmployeeById(int id)
        {
            return Task.Run(() => _employeeService.RemoveEmployee(id));
        }

        public Task AddEmployee(Employee employee)
        {
            return Task.Run(() => _employeeService.AddNewEmployee(employee));
        }

        public Task UpdateEmployee(Employee employee)
        {
            return Task.Run(() => _employeeService.UpdateEmployee(employee));
        }
    }
}
