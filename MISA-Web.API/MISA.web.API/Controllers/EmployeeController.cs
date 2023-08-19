using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Entity;
using MISA.core.Exceptions;
using MISA.core.Interface.Infrastructure;
using MISA.core.Interface.Service;
using MISA.core.Service;
using MISA.infrastructure.Repos;
using MySqlConnector;
using Dapper;

namespace MISA.web.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        // Get tất cả nhân viên
        [HttpGet]
        public IActionResult GetALl()
        {
            try
            {
                var res = _employeeRepository.GetAll();
                return StatusCode(200, res);
            }
            catch (Exception)
            {
                return BadRequest("đã xảy ra lỗi");
            }
        }

        // get nhân viên theo mã nhân viên
        [HttpGet("{EmployeeID}")]
        public IActionResult GetByID(Guid EmployeeID)
        {
            try
            {
                var res = _employeeRepository.GetById(EmployeeID);
                return StatusCode(200, res);
            }
            catch (Exception)
            {
                return BadRequest(string.Empty);
            }
        }

        //Thêm mới nhân viên
        [HttpPost]
        public IActionResult Post(Employee employee, Guid EmployeeID)
        {
            try
            {
                var res = _employeeService.InsertService(employee);
                return StatusCode(201);
            }
            catch (MISAValidateException ex)
            {
                return BadRequest("mã nhân viên khoong được phép để trống");
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Sửa thông tin nhân viên
        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Xóa nhân viên
        [HttpDelete("{EmployeeID}")]
        public IActionResult Delete(Guid EmployeeID)
        {
            try
            {
                var res = _employeeRepository.Delete(EmployeeID);
                return StatusCode(200, res);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
