using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvLab_WebApp.Models
{
    public class DescLab
    {

        [Key]
        public int ID { get; set; }

        [ForeignKey("DescCashier")]
        public int LabNo { get; set; }

        // Make DescCashier non-nullable if it's required
        public virtual DescCashier DescCashier { get; set; }
        public string? BarcodeNo { get; set; }
        public int DescID { get; set; }
        public System.DateTime RepDate { get; set; }
        public int Charges { get; set; }
        public string? DStatus { get; set; }
        public System.DateTime StatusDate { get; set; }
      
    }
}
