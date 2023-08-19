using MISA.core.Entity;
using MISA.core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Exceptions;
using MISA.core.Interface.Infrastructure;

namespace MISA.core.Service
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int InsertService(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                throw new MISAValidateException("Mã nhân viên k được phép để trống");
            }

            if (string.IsNullOrEmpty(employee.FullName))
            {
                throw new MISAValidateException("Tên nhân viên k được phép để trống");
            }

            if (string.IsNullOrEmpty(employee.Email))
            {
                throw new MISAValidateException("Email k được phép để trống");
            }

            if (string.IsNullOrEmpty(employee.NumberPhone))
            {
                throw new MISAValidateException("số điện thoại k được phép để trống");
            }

            if (string.IsNullOrEmpty(employee.IdentifyNumber))
            {
                throw new MISAValidateException("Số chứng thư k được phép để trống");
            }
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
            if (isDuplicate)
            {
                throw new MISAValidateException("Mã nhân viên đã tồn tai, vui lòng nhập mã nhân viên khác") ;  
            }
            var res = _employeeRepository.Insert(employee);
            return res;
        }

        public int UpdateService(Employee employee, Guid employeeId)
        {
            //validate dữ liệu
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                throw new MISAValidateException("Mã nhân viên k được phép để trống");
            }
            return 1;
        }
    }
}
