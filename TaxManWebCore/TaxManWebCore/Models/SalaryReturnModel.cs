namespace TaxManWebCore.Models
{
    public class SalaryReturnModel : ISalaryReturnModel
    {
        public decimal? dcGrossSal { get; set; }
        public decimal? dcNetSal { get; set; }
        public decimal? dcGrossTaxPd { get; set; }
        public decimal? dcMnthNetSal { get; set; }
        public decimal? dcMnthTaxPd { get; set; }

        public SalaryReturnModel()
        {
            dcGrossSal   = (decimal?)0.00;
            dcNetSal     = (decimal?)0.00;
            dcGrossTaxPd = (decimal?)0.00;
            dcMnthNetSal = (decimal?)0.00;
            dcMnthTaxPd  = (decimal?)0.00;
        }
    }
}
