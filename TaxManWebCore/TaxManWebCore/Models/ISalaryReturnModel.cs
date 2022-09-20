namespace TaxManWebCore.Models
{
     public interface ISalaryReturnModel
    {
         decimal? dcGrossSal { get; set; }
         decimal? dcNetSal { get; set; }
         decimal? dcGrossTaxPd { get; set; }
         decimal? dcMnthNetSal { get; set; }
         decimal? dcMnthTaxPd { get; set; }
    }
}
