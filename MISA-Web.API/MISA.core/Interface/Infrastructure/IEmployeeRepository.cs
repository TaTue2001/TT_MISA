using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entity;
namespace MISA.core.Interface.Infrastructure
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(Guid EmployeeId);
        int Insert (Employee employee); 
        int Update(Employee employee, Guid EmployeeId);
        int Delete(Guid EmployeeId);
        IEnumerable<Employee> GetPaging(int pageSize,  int pageIndex);
        bool CheckDuplicateCode(string EmployeeCode);
    }
}
