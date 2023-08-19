using MISA.core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Interface.Service
{
    public interface IEmployeeService
    {   
        //thêm mới dữ liệu
        //param name="employee"
        //createdBy: Tạ Xuân Tuệ (28/8/2023)
        int InsertService(Employee employee);

        //cập nhật dữ liệu
        //param name="employee"
        //createdBy: Tạ Xuân Tuệ (28/8/2023)
        int UpdateService(Employee employee, Guid employeeId);
    }
}
