using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entities;
using MISA.core.Interface;
using MISA.core.Interface.Infrastructure;

namespace MISA.infrastructure.Repos
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public IEnumerable<Customer> GetAll()
        {
            var customers=GetAll();
            return customers;
        }

        public Customer GetById(Guid EmployeeId)
        {
            throw new NotImplementedException();
        }
    
    }
}
