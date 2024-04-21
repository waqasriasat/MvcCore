using Syncfusion.EJ2.PdfViewer;

namespace AdvLab_WebApp.Models
{
    public class ReceptionViewModels
    {

    }

    public class CreateViewModel
    {

        public PatReg PatReg { get; set; }
        public DescCashier DescCashier { get; set; }
    }
    public class RecepViewModel
    {

        public virtual PatReg? PatReg { get; set; }
        public virtual DescCashier? DescCashier { get; set; }
    }
    public class PatRegViewModel
    {
        public PatReg PatReg { get; set; }
        public DescCashier DescCashier { get; set; }
    }
  
    public class ReportingViewModel
    {
        public PatReg PatReg { get; set; }
        public DescCashier DescCashier { get; set; }
        public DescLab DescLab { get; set; }
        public AddDescription AddDescription { get; set; }
        public AddClient AddClient { get; set; }
    }
    public class ClientDescModel
    {
        public AddClientDesc AddClientDesc { get; set; }
        public AddDescription AddDescription { get; set; }
    }
}