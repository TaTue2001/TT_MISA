using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA_Web.API.Model;
using System.Linq.Expressions;

namespace MISA_Web.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //tìm kiếm tất cả nhân viên
        [HttpGet ()]
        public IActionResult Get()
        {      
            try 
	        {	        
		        //1.khoi tao ket noi
                var connectionString = "Host=127.0.0.1; " +
                    "Port=3306; " +
                    "Database=misa_web; " +
                    "User Id=root; " +
                    "Password=tue@2001";
                var sqlConnection=new MySqlConnection(connectionString);
                //2 lay du lieu tu database
                //2.cau lenh truy van du lieu
                var query = "select*from employee";
                //2.2 thuc hien lay du lieu
                var employees = sqlConnection.Query<Employee>(sql:  query) ;
                //tra ket qua cho client    
                return Ok(employees);
	        }
	        catch (Exception ex)
	        {
                return HandleException(ex);
	        }

        }
    
        //tìm kiếm nhân viên cụ thể
        [HttpGet("{employeeID}")]
        public IActionResult GetByID(String employeeID)
        {
            try
            {
            //1.khoi tao ket noi
            var connectionString = "Host=127.0.0.1; Port=3306; Database=misa_web; User Id=root; Password=tue@2001";
            var sqlConnection = new MySqlConnection(connectionString);
            //2 lay du lieu tu database
            //2.1cau lenh truy van du lieu
            var query = $"select*from employee where EmployeeID = @EmployeeID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", employeeID);
            //2.2 thuc hien lay du lieu

            var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: query,param: parameters);
            //tra ket qua cho client    
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        //thêm mới nhân viên
        //<param name="employee">thông tin nhân viên</param>
        //<return>
        //201 - them moi thanh cong
        //400 - dữ liệu đầu vào không hợp lệ
        //500 - có exception
        //</return>
        [HttpPost] 
        public IActionResult Post([FromBody] Employee employee)
        {       

            try
            {   employee.EmployeeID=Guid.NewGuid();
                var error=new ErrorService();
                var errorData=new Dictionary<string, string>();
                //validate dữ liệu 
                  if(string.IsNullOrEmpty(employee.EmployeeCode))
                  {
                    errorData.Add("EmployeeCode",Resource.ResourceVN.Error_EmptyCode);                
                  }

                    //check xem mã nhân viên có bị trùng nhau hay k
                  if (CheckEmployeeCode(employee.EmployeeCode))
                  {
                      errorData.Add("EmployeeCode", Resource.ResourceVN.Error_EmployeeCodeDuplicate);
                  }
                  

                  if (string.IsNullOrEmpty(employee.FullName))
                  {
                      errorData.Add("FullName", Resource.ResourceVN.Error_EmptyName);
                  }
                   /*
                  if(!IsValidEmail (employee.Email))
                  {
                      errorData.Add("Email", Resource.ResourceVN.Error_ValidateEmail);
                  }*/

                  if (string.IsNullOrEmpty(employee.Email))
                  {
                      errorData.Add("Email", Resource.ResourceVN.Error_EmptyEmail);
                  }

                  if (string.IsNullOrEmpty(employee.NumberPhone))
                  {
                      errorData.Add("NumberPhone", Resource.ResourceVN.Error_EmptyNumberPhone);
                  }

                  if (string.IsNullOrEmpty(employee.IdentifyNumber))
                  {
                      errorData.Add("IdentifyNumber", Resource.ResourceVN.Error_EmptyIdenifyNumber);
                  }

                  if (errorData.Count > 0)
                  {
                      error.UserMsg ="có lỗi xảy ra";
                      error.Data=errorData;
                      return BadRequest(error);
                  }
                  
                  //khoi tao ket noi
                  var connectionString = "Host=127.0.0.1; Port=3306; Database=misa_web; User Id=root; Password=tue@2001";

                  var connection= new MySqlConnection(connectionString);
                  //2 lay du lieu tu database
                  //2.1cau lenh truy van du lieu
                  var query = "InsertEmployee";

                //mở kết nối đên database
                  connection.Open();
                //đọc các tham số đầu vào của store
                var sqlCommand=connection.CreateCommand();
                sqlCommand.CommandText=query;
                sqlCommand.CommandType=System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);
                var dynamicParam=new DynamicParameters();

                foreach (MySqlParameter parameter in sqlCommand.Parameters) {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty=employee.GetType().GetProperty(propName);

                    if (entityProperty != null)
                    {
                        var propValue=employee.GetType().GetProperty(propName).GetValue(employee);
                        dynamicParam.Add(paramName, propValue); 
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }

                }
                


                var res = connection.Execute(sql: query, param: employee, commandType: System.Data.CommandType.StoredProcedure);
                  if (res > 0)
                  {
                      return StatusCode(201, res);
                  }
                  else
                  {
                      return Ok(res);
                  }

            }
            catch (Exception ex )
            {
                return HandleException(ex);
            }
            
        }
      /*
        [HttpPut]
        public IActionResult Post([FromBody] Employee employee)
        {
            try {

                employee.EmployeeID = Guid.NewGuid();
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                //validate dữ liệu 
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resource.ResourceVN.Error_EmptyCode);
                }

                //check xem mã nhân viên có bị trùng nhau hay k
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resource.ResourceVN.Error_EmployeeCodeDuplicate);
                }


                if (string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", Resource.ResourceVN.Error_EmptyName);
                }
                
               if(!IsValidEmail (employee.Email))
               {
                   errorData.Add("Email", Resource.ResourceVN.Error_ValidateEmail);
               }

                if (string.IsNullOrEmpty(employee.Email))
                {
                    errorData.Add("Email", Resource.ResourceVN.Error_EmptyEmail);
                }

                if (string.IsNullOrEmpty(employee.NumberPhone))
                {
                    errorData.Add("NumberPhone", Resource.ResourceVN.Error_EmptyNumberPhone);
                }

                if (string.IsNullOrEmpty(employee.IdentifyNumber))
                {
                    errorData.Add("IdentifyNumber", Resource.ResourceVN.Error_EmptyIdenifyNumber);
                }

                if (errorData.Count > 0)
                {
                    error.UserMsg = "có lỗi xảy ra";
                    error.Data = errorData;
                    return BadRequest(error);
                }

                //khoi tao ket noi
                var connectionString = "Host=127.0.0.1; Port=3306; Database=misa_web; User Id=root; Password=tue@2001";

                var connection = new MySqlConnection(connectionString);
                //2 lay du lieu tu database
                //2.1cau lenh truy van du lieu
                var query = "AddEmployeeN";

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
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }*/
      
        [HttpDelete("{employeeID}")]
        public IActionResult Delete(String employeeID) 
        {
            try
            {

                    //1.khoi tao ket noi
                    var connectionString = "Host=127.0.0.1; Port=3306; Database=misa_web; User Id=root; Password=tue@2001";
                    var sqlConnection = new MySqlConnection(connectionString);
                    //2 lay du lieu tu database
                    //2.1cau lenh truy van du lieu
                    var query = $"delete from employee where EmployeeID = @EmployeeID";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@EmployeeID", employeeID);
                    //2.2 thực hiện thao tác xóa nhân viên

                    var rs = sqlConnection.Query<object>(sql: query, param: parameters);
                    //trả lại kết quả cho client    
                    return Ok("Xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        //Validate dữ liệu đầu vào

        //kiểm tra mã nhân viên có bị trùng hay không
        private bool CheckEmployeeCode (string employeeCode)
        {
            var connectionString = "Host=127.0.0.1; Port=3306; Database=misa_web; User Id=root; Password=tue@2001";
            var connection = new MySqlConnection(connectionString);
            var queryCheck = "Select EmployeeCode from misa_web.employee where EmployeeCode= @EmployeeCode";
            var dynamicParams=new DynamicParameters();
            dynamicParams.Add("@EmployeeCode", employeeCode);
            var res = connection.QueryFirstOrDefault<string>(sql:queryCheck, param: dynamicParams);
            if (res!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Xử lý ngoại lệ
        private  IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }


        //Xác thực email
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}
