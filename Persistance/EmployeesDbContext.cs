using dep_manager_singleton.Entities;

namespace dep_manager_singleton.Persistance
{
    public class EmployeesDbContext
    {
        public List<Employee> Employees { get; set; }

        public EmployeesDbContext()
        {
            Employees = new List<Employee>();
        }
    }
}
