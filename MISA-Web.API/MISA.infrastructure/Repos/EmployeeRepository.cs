using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.core.Interface.Infrastructure;
using MISA.core.Entity;

namespace MISA.infrastructure.Repos
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        const string _connectionString = "Host=127.0.0.1;Port=3306;Database=misa_web;User Id=root; Password=tue@2001";
        MySqlConnection connection;

        public EmployeeRepository()
        {

        }
        public bool CheckDuplicateCode(string EmployeeCode)
        {
            using (connection = new MySqlConnection(_connectionString))
            {
                var queryCheck = "Select EmployeeCode from misa_web.employee where EmployeeCode= @EmployeeCode";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@EmployeeCode", EmployeeCode);
                var res = connection.QueryFirstOrDefault<string>(sql: queryCheck, param: dynamicParams);
                if (res != null)
                {
                    return true;
                }
                return false;
            };

        }

        public int Delete(Guid EmployeeId)
        {
            //2 lay du lieu tu database
            //2.1cau lenh truy van du lieu
            var query = $"delete from employee where EmployeeID = @EmployeeID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", EmployeeId);
            //2.2 thực hiện thao tác xóa nhân viên

            var res = connection.Query<object>(sql: query, param: parameters);
            //trả lại kết quả cho client    
            return 1;
        }

        public IEnumerable<Employee> GetAll()
        {
            using (connection = new MySqlConnection(_connectionString))
            {
                //2 lay du lieu tu database
                //2.cau lenh truy van du lieu
                var query = "select*from employee";
                //2.2 thuc hien lay du lieu
                var employees = connection.Query<Employee>(sql: query);
                return employees;
            }

        }

        public Employee GetById(Guid EmployeeId)
        {
            using (connection = new MySqlConnection(_connectionString))
            {
                //2 lay du lieu tu database
                //2.1cau lenh truy van du lieu
                var query = $"select*from employee where EmployeeID = @EmployeeID";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", EmployeeId);
                //2.2 thuc hien lay du lieu

                var employee = connection.QueryFirstOrDefault<Employee>(sql: query, param: parameters);  
                return (employee);
            }

        }

        public IEnumerable<Employee> GetPaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee employee)
        {
            using (connection = new MySqlConnection(_connectionString))
            {
                //2 lay du lieu tu database
                //2.1cau lenh truy van du lieu
                var query = "InsertEmployee";

                //mở kết nối đên database
                connection.Open();
                //đọc các tham số đầu vào của store
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);
                var dynamicParam = new DynamicParameters();

                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = employee.GetType().GetProperty(propName);

                    if (entityProperty != null)
                    {
                        var propValue = employee.GetType().GetProperty(propName).GetValue(employee);
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }

                }

                var res = connection.Execute(sql: query, param: employee, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }

        }

        public int Update(Employee employee, Guid EmployeeId)
        {
            using (connection = new MySqlConnection(_connectionString))
            {

            }
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
