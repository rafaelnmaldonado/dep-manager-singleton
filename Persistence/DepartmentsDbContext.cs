using dep_manager_singleton.Entities;

namespace dep_manager_singleton.Persistence
{
    public class DepartmentsDbContext
    {
        public List<Department> Departments { get; set; }

        public DepartmentsDbContext()
        {
            Departments = new List<Department>();
        }
    }
}
