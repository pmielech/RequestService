using Microsoft.VisualBasic;

namespace RequestService.Data
{
  public class GymPriceInfoType
  {
    public uint Id { get; set; }
    public string GymName { get; set; } = String.Empty;
    public decimal PricePerMonth { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
    public decimal TotalMonths { get; set; } = 0;

    public string PromoDescription { get; set; } = String.Empty;
  }

}
