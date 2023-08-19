using MISA_Web.API.Model;
namespace MISA_Web.API.Model
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; } 
        public string NumberPhone { get; set; }
        public string IdentifyNumber { get; set; }
        public DateTime? IdentifyDate { get; set; }
        public string? IdentifyPlace { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string? TaxCode { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public Guid? DepartmentID { get; set; }
        public Guid? PositionID { get; set;}
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return "nữ";
                    case 1:
                        return "nam";
                    case 2:
                        return "k xác định";
                    default:
                        return "unknown"; 
                }
            }
        }
    }
}
