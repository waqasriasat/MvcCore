using System.ComponentModel.DataAnnotations;

namespace AdvLab_WebApp.Models.Temp
{
    public class UserList
    {
        public int Id { get; set; }
        public string? LoginStatus { get; set; }
        public string? LoginStatusIp { get; set; }
        public string? LoginStatusComp { get; set; }
        public string? Nsend { get; set; }
        public DateTime? RegDate { get; set; }
        public int EmpId { get; set; }
        public string? Uname { get; set; }
        [DataType(DataType.Password)]
        public string? Upassword { get; set; }
        public string? ClientV { get; set; }
        public string? Status { get; set; }
        public string? Place { get; set; }
        public string? PlaceCategory { get; set; }
        public string? Cnl { get; set; }
        public string? Location { get; set; }
        public string? Depart { get; set; }
        public string? SubDepart { get; set; }
        public string? Placement { get; set; }
        public string? Designation { get; set; }
        public int? Uid { get; set; }
        public string? Idloc { get; set; }
        public string? CompMac { get; set; }
        public string? PayGenerate { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Token { get; set; }

    }
}
