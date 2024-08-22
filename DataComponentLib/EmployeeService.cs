using DataComponentLib.Data;

namespace DataComponentLib
{
    public interface IEmployeeService
    {
        void AddNewEmployee(Employee employee);
        void RemoveEmployee(int empId);

        void UpdateEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int empId);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly EmpDbContext _context;
        public EmployeeService(EmpDbContext context)
        {
            _context = context;
        }

        public void AddNewEmployee(Employee employee)
        {
            _context.Employees.Add(employee); 
            _context.SaveChanges();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        private Employee getEmployee(int empId)
        {
            var rec = _context.Employees.FirstOrDefault(e => e.EmpId == empId);
            if (rec == null)
            {
                throw new Exception("Employee not found");
            }
            return rec;
        }
        public Employee GetEmployeeById(int empId)
        {
            return getEmployee(empId);
        }

        public void RemoveEmployee(int empId)
        {
            var rec = getEmployee(empId);
            _context.Employees.Remove(rec);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            var rec = getEmployee(employee.EmpId);
            rec.EmpName = employee.EmpName;
            rec.EmpAddress = employee.EmpAddress;
            rec.EmpSalary = employee.EmpSalary;
            _context.SaveChanges();
        }
    }
}
