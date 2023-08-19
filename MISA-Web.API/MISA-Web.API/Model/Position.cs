namespace MISA_Web.API.Model
{
    public class Position
    {
        //public string PositionID { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedDate { get; set;}
        public string ModifiedBy { get; set;}
    }
}
